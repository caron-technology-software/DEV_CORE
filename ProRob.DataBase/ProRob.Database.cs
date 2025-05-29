using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProRob.DataBase
{
    public struct DatabaseSettings
    {
        public string server;
        public string database;
        public string username;
        public string password;
        public string certificatePath;
        public string certificatePassword;

        public DatabaseSettings(string server, string database, string username, string password)
        {
            this.server = server;
            this.database = database;
            this.username = username;
            this.password = password;
            this.certificatePath = null;
            this.certificatePassword = null;
        }

        public DatabaseSettings(string server, string database, string username, string password, string certificatePath, string certificatePassword)
        {
            this.server = server;
            this.database = database;
            this.username = username;
            this.password = password;
            this.certificatePath = certificatePath;
            this.certificatePassword = certificatePassword;
        }
    }
}
