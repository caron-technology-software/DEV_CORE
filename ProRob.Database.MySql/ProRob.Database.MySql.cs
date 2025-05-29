using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;


namespace ProRob.DataBase.MySql
{
    public class Database
    {
        private readonly MySqlConnection dbConn;

        private readonly string database;
        private readonly string server;
        private readonly string username;
        private readonly string password;
        private readonly string certificatePath;
        private readonly string certificatePassword;
        private bool SSLConnection { get; }

        private readonly object locker = new object();

        public bool IsInitialized { get; set; } = false;

        public string GetServerVersion => IsInitialized ? dbConn.ServerVersion : "";

        public ConnectionState ConnectionState => dbConn.State;

        public string DatabaseName => database;
        public string Server => server;

        public Database(DatabaseSettings settings)
            : this(settings.server, settings.database, settings.username, settings.password, settings.certificatePath, settings.certificatePassword)
        {
            // --
        }

        public Database(string server, string database, string username, string password)
            : this(server, database, username, password, "", "")
        {
            // --
        }

        public Database(string server, string database, string username, string password, string certificatePath, string certificatePassword)
        {
            this.server = server;
            this.database = database;
            this.username = username;
            this.password = password;
            this.certificatePath = certificatePath;
            this.certificatePassword = certificatePassword;

            SSLConnection = !((String.IsNullOrEmpty(this.certificatePath) || String.IsNullOrEmpty(this.certificatePassword)));

            if (SSLConnection == false)
            {
                dbConn = new MySqlConnection
                    (
                        String.Format("Persist Security Info=False;server={0};database={1};uid={2};password={3};SSL Mode=None",
                                      this.server, this.database, this.username, this.password)
                    );
            }
            else
            {
                dbConn = new MySqlConnection
                    (
                       String.Format("Persist Security Info=False;server={0};database={1};uid={2};password={3};" +
                                     "CertificateFile={4};" +
                                     "CertificatePassword={5};" +
                                     "SSL Mode=Required",
                                     this.server, this.database, this.username, this.password,
                                     this.certificatePath,
                                     this.certificatePassword)
                    );
            }
            try
            {
                dbConn.Open();
                IsInitialized = true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"[EXCEPTION on MySqlConnection]\n\tMessage: {e.Message}\n\tSource: {e.Source}");
            }
            finally
            {
                dbConn.Close();
            }
        }

        public bool TryDumpDatabase(string mySqlBinFolder, string pathDump)
        {
            string cmdParameters = "";

            try
            {
                cmdParameters = String.Format("-u {0} -p\"{1}\" {2} --result-file=\"{3}\"", username, password, database, pathDump);
                ProcessHelper.Execute($"{mySqlBinFolder}mysqldump", cmdParameters);
                return true;
            }
            catch
            {
                Console.WriteLine($"ERROR: {mySqlBinFolder}mysqldump {cmdParameters}");
                return false;
            }
        }

        public string GetDatabaseName()
        {
            if (TryExecuteSqlQuery($"SELECT DATABASE();", out List<List<string>> data))
            {
                return data[0].First();
            }
            else
            {
                return null;
            }
        }

        public string GetTableSyntax(string table)
        {
            if (TryExecuteSqlQuery($"SHOW CREATE TABLE {table};", out List<List<string>> data))
            {
                return data[0][1];
            }
            else
            {
                return null;
            }
        }

        public DataTable GetTableIndexes(string table)
        {
            if (TryFillDataTable($"SHOW INDEX FROM {table};", out DataTable dataTable))
            {
                return dataTable;
            }
            else
            {
                return null;
            }
        }

        public DataTable GetTableDescription(string table)
        {
            if (TryFillDataTable($"DESCRIBE {table};", out DataTable dataTable))
            {
                return dataTable;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<string> GetTablesNames()
        {
            if (TryExecuteSqlQuery("SHOW TABLES;", out List<List<string>> dataOutput))
            {
                return dataOutput.SelectMany(x => x);
            }
            else
            {
                return null;
            }

        }

        public IEnumerable<string> GetColumns(string table)
        {
            if (TryExecuteSqlQuery($"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{table}';", out List<List<string>> dataOutput))
            {
                return dataOutput.SelectMany(x => x);
            }
            else
            {
                return null;
            }

        }

        public bool RemoveColumn(string table, string column)
        {
            return TryExecuteSqlQuery($"ALTER TABLE `{table}` DROP `{column}`;");
        }

        public string GetColumnType(string table, string column)
        {
            if (TryExecuteSqlQuery($"SELECT DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = '{table}' AND COLUMN_NAME = '{column}';", out List<List<string>> dataOutput))
            {
                return dataOutput[0].First();
            }
            else
            {
                return null;
            }

        }

        public string GetCreateTable(string table)
        {
            if (TryExecuteSqlQuery($"SHOW CREATE TABLE {table};", out List<List<string>> dataOutput))
            {
                return dataOutput[0][1];
            }
            else
            {
                return null;
            }

        }

        public int GetIntFromQuery(string query)
        {
            int value = -1;

            bool ret = TryExecuteSqlQuery(query, out List<List<string>> resQuery);

            //Database.PrintTable(resQuery);

            try
            {
                if (ret && resQuery != null && resQuery.Count > 0)
                {
                    int.TryParse(resQuery[0][0], out value);
                }
            }
            catch //(Exception ex)
            {
                //Console.WriteLine(ex.Message);
            }

            return value;
        }

        public bool TryExecuteSqlQuery(string strQuery)
        {
            if (!IsInitialized)
            {
                return false;
            }
            try
            {
                lock (locker)
                {
                    dbConn.Open();

                    MySqlCommand dbCmd = new MySqlCommand(strQuery, dbConn);
                    dbCmd.ExecuteNonQuery();

                    dbConn.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                dbConn.Close();
            }
        }

        public bool TryExecuteSqlQuery(string strQuery, out long lastInsertedID)
        {
            lastInsertedID = -1;

            if (!IsInitialized)
            {
                return false;
            }
            try
            {
                lock (locker)
                {
                    dbConn.Open();

                    MySqlCommand dbCmd = new MySqlCommand(strQuery, dbConn);
                    dbCmd.ExecuteNonQuery();
                    lastInsertedID = dbCmd.LastInsertedId;

                    dbConn.Close();
                }
                return false;
            }
            catch
            {
                return true;
            }
            finally
            {
                dbConn.Close();
            }
        }

        public bool TryExecuteSqlQuery(string strQuery, out List<List<string>> dataOutput)
        {
            if (!IsInitialized)
            {
                dataOutput = null;
                return false;
            }
            try
            {
                int currentRow = 0;
                dataOutput = new List<List<string>>();

                lock (locker)
                {
                    dbConn.Open();
                    MySqlCommand dbCmd = new MySqlCommand(strQuery, dbConn);
                    MySqlDataReader dbReader = dbCmd.ExecuteReader();

                    while (dbReader.Read())
                    {
                        dataOutput.Add(new List<string>());
                        for (int i = 0; i < dbReader.FieldCount; i++)
                        {
                            try
                            {
                                dataOutput[currentRow].Add(dbReader.GetString(i));
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                dataOutput[currentRow].Add("null");
                            }
                        }
                        currentRow++;
                    }
                    dbConn.Close();
                }
                return true;
            }
            catch
            {
                dataOutput = null;
                return false;
            }
            finally
            {
                dbConn.Close();
            }
        }

        public bool TryFillDataTable(string strQuery, out DataTable dataOutput)
        {
            if (!IsInitialized)
            {
                dataOutput = null;
                return false;
            }
            try
            {
                DataTable table = new DataTable("GenericTable");

                lock (locker)
                {
                    MySqlCommand dbCmd = new MySqlCommand(strQuery, dbConn);
                    MySqlDataAdapter dump = new MySqlDataAdapter(dbCmd);
                    dbConn.Open();

                    dump.Fill(table);

                    dbConn.Close();
                }

                dataOutput = table;

                return true;
            }
            catch
            {
                dataOutput = null;
                dbConn.Close();

                return false;
            }
        }
    }
}