using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Xml.XPath;

using ProRob;
using ProRob.Security;

//using Caron.Lion.Control.LowLevel.PLC;

namespace Caron.FileFormats.CutTicketX
{
    public class CutTicketHandlerParameters
    {
        public string ctx_path_default_i { get; set; }
        public string ctx_name_default_i { get; set; }
        public string ctx_path_default_o { get; set; }
        public string ctx_name_default_o { get; set; }
        public string ctx_path_default_b { get; set; }
    }

    public class CutTicketHelper
    {
        public const int NumberOfRows = 50;
        public const int NumberOfColumns = 50;
        //private const string Path1 = @"C:\CARON\machine_lion\";

        public static void SetPliesDoneVector(ushort[] vector)
        {
            ushort[,] matrix = new ushort[NumberOfRows, NumberOfColumns];

            for (int r = 0; r < vector.Length; r++)
            {
                matrix[r, 0] = vector[r];
            }

            ////Tex.FabricSizes.SetPliesDoneDataTableValues(CoreSettings.IpAddressPlc, matrix);
        }

        public static T[] GetRowFromMatrix<T>(T[,] matrix, int row)
        {
            int nRows = matrix.GetLength(0);

            T[] vector = new T[nRows];

            for (int i = 0; i < nRows; i++)
            {
                vector[i] = matrix[row, i];
            }

            return vector;
        }

        public static int GetPliesDone()
        {
            ////Tex.FabricSizes.GetDataTableValues(CoreSettings.IpAddressPlc, Tex.FabricSizes.DataTableType.SizesDone, out ushort[,] dataTableSizesDone);

            return 0; ////GetRowFromMatrix(dataTableSizesDone, 0).ToList().Sum(x => x);
        }

        public static int GetPliesToDo()//KO
        {
            ////Tex.FabricSizes.GetDataTableValues(CoreSettings.IpAddressPlc, Tex.FabricSizes.DataTableType.SizeTodo, out ushort[,] dataTableSizesTodo);

            return 0;////GetRowFromMatrix(dataTableSizesTodo, 0).ToList().Sum(x => x);//tockeck versione multicolore
        }

        public static int[] CopyVector(int[] vector)
        {
            int[] vectorCopy = new int[vector.Length];


            for (int i = 0; i < vector.Length; i++)
            {
                vectorCopy[i] = vector[i];
            }

            return vectorCopy;
        }

        public static int[] GetVectorPliesDone()
        {
            int nRows = 0;////Lion.Control.LowLevel.PLC.Constants.MatrixDimension;

            ////Tex.FabricSizes.GetDataTableValues(CoreSettings.IpAddressPlc, Tex.FabricSizes.DataTableType.SizesDone, out ushort[,] dataTableSizesDone);

            int[] vector = new int[nRows];

            for (int i = 0; i < nRows; i++)
            {
                vector[i] = 0;////GetRowFromMatrix(dataTableSizesDone, i).ToList().FirstOrDefault();
            }

            return vector;
        }

        public static int[] GetVectorPliesToDo()
        {
            int nRows = 0;////LowLevel.PLC.Constants.MatrixDimension;

            ////Tex.FabricSizes.GetDataTableValues(CoreSettings.IpAddressPlc, Tex.FabricSizes.DataTableType.SizeTodo, out ushort[,] dataTableSizesTodo);

            int[] vector = new int[nRows];

            for (int i = 0; i < nRows; i++)
            {
                vector[i] = 0;////GetRowFromMatrix(dataTableSizesTodo, i).ToList().FirstOrDefault();
            }

            return vector;
        }

        public static bool TryGetColorIndexDone(int[] precedentVector, int[] currentVector, out int nPlies, out int colorIndex)
        {
            int n = currentVector.Length;

            nPlies = 0;
            colorIndex = 0;

            int i;

            var list = new List<int>();

            for (i = 0; i < n; i++)
            {
                list.Add(currentVector[i] - precedentVector[i]);
            }

            i = 0;
            while (i < n)
            {
                if (list[i] > 0)
                {
                    colorIndex = i;
                    nPlies = list[i];

                    return true;
                }

                i++;
            }

            return false;
        }

        /* Analoga funzione per il colore */
        // TODO tocheck
        public static int GetColorToDo()
        {
            ////Tex.FabricSizes.GetDataTableValues(CoreSettings.IpAddressPlc, Tex.FabricSizes.DataTableType.SizeTodo, out ushort[,] dataTableSizesTodo);

            return 0;////GetRowFromMatrix(dataTableSizesTodo, 0).ToList().Sum(x => x);//tockeck versione multicolore
        }

        ////public static void UpdateWorkingCutFiles(ref HashedFilesCollection collectionFileCutFile,
        ////                                         FileFilter[] fileCutFileFilter,
        ////                                         ref List<WorkingCutTicketFile> workingFiles,

        ////                                         out HashedFilesCollection updatedCollectionFileCutFile,
        ////                                         out List<WorkingCutTicketFile> workingsToAdd,
        ////                                         out List<WorkingCutTicketFile> workingsToRemove)
        ////{
        ////    updatedCollectionFileCutFile = new HashedFilesCollection(Hashing.ComputeSHA512, fileCutFileFilter);

        ////    var files = Directory.GetFiles(ControlContext.ProductionOrdersFolder, "*.CTX", SearchOption.AllDirectories);

        ////    //GPIx99
        ////    if (MachineSettings.MaxNumberOfProductionOrdersToProcess > 0)
        ////    {
        ////        if (files.Count() > MachineSettings.MaxNumberOfProductionOrdersToProcess)
        ////        {
        ////            files = files.Take(MachineSettings.MaxNumberOfProductionOrdersToProcess).ToArray();
        ////        }
        ////    }
        ////    //GPFx99


        ////    updatedCollectionFileCutFile.AddFiles(files);

        ////    var dictionaryCollectionFiles = collectionFileCutFile.GetFilesDictionary().ToDictionary(x => x.Key, x => x.Value);
        ////    var dictionaryScannedCollectionFiles = updatedCollectionFileCutFile.GetFilesDictionary().ToDictionary(x => x.Key, x => x.Value);

        ////    var collectionFiles = dictionaryCollectionFiles.Select(x => x.Key);
        ////    var scannedCollectionFiles = dictionaryScannedCollectionFiles.Select(x => x.Key);

        ////    var elementsCommon = scannedCollectionFiles.Intersect(collectionFiles).ToList();
        ////    var elementsToAdd = scannedCollectionFiles.Except(collectionFiles).ToList();
        ////    var elementsToRemove = collectionFiles.Except(scannedCollectionFiles).ToList();

        ////    workingsToAdd = new List<WorkingCutTicketFile>();
        ////    workingsToRemove = new List<WorkingCutTicketFile>();


        ////    if (elementsToAdd.Count() > 0)
        ////    {
        ////        Console.WriteLine("ADD");
        ////        elementsToAdd.ForEach(x => { Console.WriteLine(x); });
        ////    }

        ////    if (elementsToRemove.Count() > 0)
        ////    {
        ////        Console.WriteLine("REMOVE");
        ////        elementsToRemove.ForEach(x => { Console.WriteLine(x); });
        ////    }

        ////    foreach (var item in elementsToAdd)
        ////    {
        ////        var element = dictionaryScannedCollectionFiles[item];

        ////        var working = new WorkingCutTicketFile()
        ////        {
        ////            FileInfo = element.FileInfo,
        ////            Hash = element.Hash,
        ////            //GerberFile = "missing.gbr",
        ////            Barcode = Barcode.GetBarcodeFromHash($"{element.FileInfo.FullName}")
        ////        };

        ////        workingFiles.Add(working);

        ////        workingsToAdd.Add(working);
        ////    }

        ////    foreach (var item in elementsToRemove)
        ////    {
        ////        var element = dictionaryCollectionFiles[item];

        ////        var cutFileInfoHashToRemove = workingFiles.Find(x => x.FileInfo.FullName == element.FileInfo.FullName);

        ////        int indexToRemove = workingFiles.FindIndex(x => x.FileInfo.FullName == element.FileInfo.FullName);

        ////        if (indexToRemove >= 0)
        ////        {
        ////            workingFiles.RemoveAt(indexToRemove);
        ////        }

        ////        workingsToRemove.Add(cutFileInfoHashToRemove);
        ////    }
        ////}

        public static void SetSpreadMode(CutTicket cutTicket)
        {
            //TODO : ETC fare uno switch
            if (cutTicket.MarkerReport.SpreadMode.Equals("FaceToFace"))
            {
                ////Tex.Enviroment.SetFaceToFaceMode(CoreSettings.IpAddressPlc);
            }

            if (cutTicket.MarkerReport.SpreadMode.Equals("SinglePly"))
            {
                ////Tex.Enviroment.SetOpenSpreadMode(CoreSettings.IpAddressPlc);
            }
        }

        public static bool IsCutFile(string fullPath)
        {
            try
            {
                var cutTicket = CutTicketHelper.DeserializeCutTicket(fullPath);

                if (cutTicket is null)
                {
                    return false;
                }

                if (cutTicket.SpreadList.Select(x => x.Ply - x.PliesSpread).Any(x => x > 0))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;

            }
        }

        public static CutTicket DeserializeCutTicket(string filename)
        {
            CutTicket cutTicket = null;

            string xml = File.ReadAllText(filename);
            XmlSerializer serializer = new XmlSerializer(typeof(CutTicket));
            using (StringReader reader = new StringReader(xml))
            {
                if (reader != null)
                {
                    if (!string.IsNullOrEmpty(xml))
                    {
                        cutTicket = (CutTicket)serializer.Deserialize(reader);
                    }
                }
            }

            return cutTicket;
        }

        public static string SerializeCutTicket(CutTicket cutTicket)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CutTicket));
            MemoryStream stream = new MemoryStream();
            Encoding utf8noBOM = new UTF8Encoding(false);

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.Encoding = utf8noBOM;
            //settings.OmitXmlDeclaration = true;
            settings.NewLineOnAttributes = false;

            //XmlWriter writer = new XmlTextWriter(ms, Encoding.UTF8);//unidode
            XmlWriter writer = XmlWriter.Create(stream, settings);
            serializer.Serialize(writer, cutTicket);

            string xml = ReadStreamInChunks(stream);

            //writer.Close();
            stream.Close();

            return xml;
        }

        private static string ReadStreamInChunks(Stream stream, int chunkLength = 2048)
        {
            stream.Seek(0, SeekOrigin.Begin);
            string result;
            using (var textWriter = new StringWriter())
            using (var reader = new StreamReader(stream))
            {
                var readChunk = new char[chunkLength];
                int readChunkLength;
                //do while: is useful for the last iteration in case readChunkLength < chunkLength
                do
                {
                    readChunkLength = reader.ReadBlock(readChunk, 0, chunkLength);
                    textWriter.Write(readChunk, 0, readChunkLength);
                } while (readChunkLength > 0);

                result = textWriter.ToString();
            }

            return result;
        }

        public static void CalculatesPliesToDoVector(CutTicket cutTicket, out ushort[] start)
        {
            int[] PliesSpreadS = null;//-> int todo
            int[] Ply = null;// -> int todo

            int n_SpreadList = cutTicket.SpreadList.ToList().Count;

            PliesSpreadS = new int[n_SpreadList];
            Ply = new int[n_SpreadList];
            start = new ushort[n_SpreadList];
            for (int i = 0; i < n_SpreadList; i++)
            {
                PliesSpreadS[i] = (cutTicket.SpreadList.ToList())[i].PliesSpread;
                Ply[i] = (cutTicket.SpreadList.ToList())[i].Ply;
                start[i] = (ushort)(PliesSpreadS[i]);//Ply[i] -
                if (PliesSpreadS[i] > Ply[i])
                {   //[GB]non aggiorno: esempio ctx.Ply 70 da fare e ctx.PliesSpread 75 fatti.
                    //                    delta[i] = (ushort)(PliesSpreadS[i] - Ply[i]);//ko se 41 fatti 40 da fare scrive 1.
                    //delta[i] = PliesSpreadS[i];
                    //delta[i] = (ushort)(PliesSpreadS[i]);//ko se 41 fatti 40 da fare scrive 1.
                    //delta[i] = (ushort)(Ply[i]);
                    start[i] = (ushort)(PliesSpreadS[i]);
                }
            }
        }

        public static void CalculatesPliesDoneVector(CutTicket cutTicket, out ushort[] delta)
        {
            int[] PliesSpreadS = null;//-> int todo
            int[] Ply = null;// -> int todo

            int n_SpreadList = cutTicket.SpreadList.ToList().Count;

            PliesSpreadS = new int[n_SpreadList];
            Ply = new int[n_SpreadList];
            delta = new ushort[n_SpreadList];
            for (int i = 0; i < n_SpreadList; i++)
            {
                PliesSpreadS[i] = (cutTicket.SpreadList.ToList())[i].PliesSpread;
                Ply[i] = (cutTicket.SpreadList.ToList())[i].Ply;
                delta[i] = (ushort)(PliesSpreadS[i]);//Ply[i] -
                if (PliesSpreadS[i] > Ply[i])
                {   //[GB]non aggiorno: esempio ctx.Ply 70 da fare e ctx.PliesSpread 75 fatti.
                    //                    delta[i] = (ushort)(PliesSpreadS[i] - Ply[i]);//ko se 41 fatti 40 da fare scrive 1.
                    //delta[i] = PliesSpreadS[i];
                    //delta[i] = (ushort)(PliesSpreadS[i]);//ko se 41 fatti 40 da fare scrive 1.
                    //delta[i] = (ushort)(Ply[i]);
                    delta[i] = (ushort)(PliesSpreadS[i]);
                }
            }
        }//calcola

        /*
         * n.b lo faccio sulla matrice, primo giro forse andrebbe meglio su xml (forse)
         */
        public static void CalculatesInversePliesDoneVector(CutTicket cutTicket, out ushort[] delta_r)
        {
            delta_r = null;
            int[] PliesSpreadS = null;//-> int todo
            int[] Ply = null;// -> int todo

            if (cutTicket != null)
            {
                int n_SpreadList = cutTicket.SpreadList.ToList().Count;

                PliesSpreadS = new int[n_SpreadList];
                Ply = new int[n_SpreadList];
                delta_r = new ushort[n_SpreadList];
                for (int i = 0; i < n_SpreadList; i++)
                {
                    PliesSpreadS[i] = (cutTicket.SpreadList.ToList())[i].PliesSpread;
                    Ply[i] = (cutTicket.SpreadList.ToList())[i].Ply;
                    delta_r[i] = (ushort)(Ply[i] - PliesSpreadS[i]);//
                    if (PliesSpreadS[i] > Ply[i])
                    {
                        delta_r[i] = 0;
                    }
                }
            }//calcola inversa ...
        }

////        public static void CutTicketStub(String state, string nteli_aggiungere, string ID, int COLORID, string ctx_path_default_i, string ctx_name_default_i, string barcode)
////        {
////            //Console.WriteLine(" TEST_Gerber_XML ");
////            //dichiaro le variabili
////            //String state;// start SpreadingInProgress IsSpread
////            //String nteli_aggiungere = "0";
////            //String ID; //seriale dello spread
////            String filename_i;
////            //String filename_o;
////            String filename_b;
////            String filename_log;
////            String BarcodePath;
////            String xml;
////            String dt;
////            String dt_log;
////            String[] StepColor;
////            int[] PliesSpread;//-> int todo
////            String[] PliesSpreadS;//-> int todo
////            int[] Ply;// -> int todo
////            int nxSpreadProgressList = 0;
////            String JobFile = null;
////            //filename_x -> su json
////            String Path_i = null;
////            String file_i = null;
////            //String Path_o = null;
////            //String file_o = null;
////            //String Path_l = null;
////            String File_l = null;
////            int n_s = 0;
////            CutTicketHandlerParameters obj_parametri = null;
////            //String fileReaderP = "";

////            //->todo valutare se spostare su json
////            string TicketStatus = null;
////            //            string WorkOrderFile = null;
////            //            String WorkOrderName = null;
////            //...altri parametri del ctx
////            //int COLORID = 0;
////            //string cutpath;
////            //string foldername;
////            //string ctx_path_default_i;
////            //string ctx_name_default_i;
////            string ctx_path_default_o = "";
////            string ctx_name_default_o = "";
////            string ctx_path_default_b = "";
////            //string ctx_name_default_b = "";//todo contatore valutare se spostare su json r/w o parametro DB
////            //bool ctx_debug;


////            #region parametri_json
////            obj_parametri = ProRob.Json.Deserialize<CutTicketHandlerParameters>(File.ReadAllText(Lion.Constants.Path.CutTicketHandlerParametersPath));

////            if (obj_parametri == null) { Console.WriteLine("Caricare file parametri"); }

////            //cutpath = obj_parametri.CutFolderName;

////            //if (args[4] != null)
////            if (ctx_path_default_i != null)
////            {
////                //ctx_path_default_i = args[4];//folder
////                //ctx_name_default_i = args[5];//filename
////                //ctx_path_default_o = args[4];
////                //ctx_name_default_o = args[5];
////                ctx_path_default_b = obj_parametri.ctx_path_default_b;//toask
////                //ctx_name_default_b = obj_parametri.ctx_name_default_b;
////            }
////            else
////            {
////                ctx_path_default_i = obj_parametri.ctx_path_default_i;
////                ctx_name_default_i = obj_parametri.ctx_name_default_i;
////                ctx_path_default_o = obj_parametri.ctx_path_default_o;
////                ctx_name_default_o = obj_parametri.ctx_name_default_o;
////                ctx_path_default_b = obj_parametri.ctx_path_default_b;
////                //ctx_name_default_b = obj_parametri.ctx_name_default_b;
////            }

////            ctx_path_default_b = obj_parametri.ctx_path_default_b;
////            //ctx_name_default_b = obj_parametri.ctx_name_default_b;
////            //ctx_debug = obj_parametri.Debug;
////            //foldername = obj_parametri.FolderName;
////            #endregion


////            //TODO lista per caricare i file da processare. far vedere risp a tappa.
////            dt = DateTime.Now.ToString("yyyy-MM-dd") + 'T' + DateTime.Now.ToString("hh:mm:ss") + 'Z';
////            dt_log = DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss");

////            #region Path
////            //Altri possibili parametri.
////            //eventualmente parametrizzare con parametri_cutticket: ho notato che la cartella di lavoro c:\cut\
////            //(...)
////            Path_i = ctx_path_default_i; //@"C:\CARON\machine_lion\cut_ticket\";//
////            file_i = ctx_name_default_i; //@"CutTicket.CTX";//
////            filename_i = Path.Combine(Path_i, file_i);
////            if (!Directory.Exists(Path.GetDirectoryName(filename_i)))
////            {
////                Directory.CreateDirectory(filename_i);
////            }

////            //Path_o = ctx_path_default_o; //@"C:\CARON\machine_lion\cut_ticket\";//
////            //file_o = ctx_name_default_o; //@"CutTicket.CTX";//
////            //filename_o = Path.Combine(Path_o, file_o);
////            //if (!Directory.Exists(Path.GetDirectoryName(filename_o)))
////            //{
////            //    Directory.CreateDirectory(filename_o);
////            //}

////            File_l = file_i + dt_log + ".CTX";
////            filename_log = Path.Combine(Lion.Constants.Path.MachineFolder, "log", File_l);
////            //filename_log = Path_i + @"log\" + file_i + dt_log + ".CTX"; // TODO path combine
////            if (!Directory.Exists(Path.GetDirectoryName(filename_log)))
////            {
////                Directory.CreateDirectory(filename_log);
////            }

////            filename_b = ctx_path_default_b; //@"C:\CARON\machine_lion\cut_ticket\";
////            if (!Directory.Exists(Path.GetDirectoryName(filename_b)))
////            {
////                Directory.CreateDirectory(filename_b);
////            }

////            BarcodePath = Path.Combine(filename_b, barcode);

////            #endregion


////            var cutTicket = DeserializeCutTicket(filename_i);//watch
////            //TODO recuperare tutte le variabili
////            JobFile = cutTicket.JobFile.Trim().ToString();
////            //order
////            TicketStatus = cutTicket.TicketStatus.Trim().ToString();

////            #region StepColor
////            // ciclo sugli spread per popolare color
////            int n_SpreadList = cutTicket.SpreadList.ToList().Count;
////            StepColor = new string[n_SpreadList];
////            PliesSpread = new int[n_SpreadList];
////            PliesSpreadS = new string[n_SpreadList];
////            Ply = new int[n_SpreadList];
////            for (int i = 0; i < n_SpreadList; i++)
////            {
////                StepColor[i] = (cutTicket.SpreadList.ToList())[i].Color;
////                PliesSpreadS[i] = (cutTicket.SpreadList.ToList())[i].PliesSpread.ToString();
////                Ply[i] = (cutTicket.SpreadList.ToList())[i].Ply;
////            }
////            #endregion StepColor

////            switch (state)
////            {
////                case "start":

////                    //devo vedere se esiste spreadProgressList

////                    if (cutTicket.SpreadProgressList != null)
////                    {
////                        nxSpreadProgressList = cutTicket.SpreadProgressList.ToList().Count();
////                    }
////                    else
////                    {
////                        nxSpreadProgressList = 0;
////                    }


////                    cutTicket.SpreadList[0].PliesSpread += Convert.ToByte(nteli_aggiungere);//1
////                    CreateNodeSpreadProgressListDeparture(filename_i, filename_i, barcode, StepColor, PliesSpreadS, ID, cutTicket.SpreadList[0].PliesSpread.ToString(), nteli_aggiungere, nxSpreadProgressList);//todo rivedere parametri
////                    NodeBarcodeCutTicket(JobFile, BarcodePath, file_i);

////                    break;

////                case "SpreadingInProgress": //OK

////                    //TODO qua o a livello di ciclo?
////                    //deve sempre esserci uno start iniziale.
////                    //lascio LIBERO?
////                    if (cutTicket.SpreadProgressList == null)
////                    {
////                        n_s = cutTicket.SpreadProgressList.Count() - 1;
////                    }
////                    else
////                    {
////                        n_s = cutTicket.SpreadProgressList.Count() - 1;
////#if (DEBUG && TRACE)
////                        Console.Error.WriteLine("spreadprogresslist count" + n_s);
////#endif
////                    }

////                    cutTicket.TicketStatus = "InProgress";//rindondante per sicurezza
////                    var spreadListLastM = cutTicket.SpreadList.ToList();

////                    //NB colore STEPID oltre il limite
////                    if (COLORID > cutTicket.SpreadList.Count() - 1)
////                    {
////                        Console.Error.WriteLine("errore ? COLORID > COUNT");//da gestire per il momento arrivo qua dai miei test
////                        COLORID = cutTicket.SpreadList.Count() - 1;
////                    }

////                    cutTicket.SpreadList[COLORID].PliesSpread += Convert.ToInt32(nteli_aggiungere);//1
////                    cutTicket.SpreadList = spreadListLastM.ToArray();
////                    cutTicket.SpreadProgressList[n_s].SpreadingProgress.PliesSpread += Convert.ToInt32(nteli_aggiungere);//1

////                    /* aggiornare time stamp una volta sola */
////                    /*NB osservazione mike silva in risposta mail del 8/2*/

////                    if (cutTicket.SpreadProgressList.ToList().Last().SpreadingProgress.PliesSpread < 2)
////                    {
////                        //Console.WriteLine("[SpreadingProgress.Statuses] timeStamp");
////                        foreach (var status in cutTicket.SpreadProgressList.ToList().Last().SpreadingProgress.Statuses.ToList())
////                        {
////                            if (status.Value.Equals("SpreadingInProgress"))
////                            {
////                                //Console.WriteLine($"Timestamp:{status.timeStamp} Value:{status.Value}");
////                                status.timeStamp = dt;
////                            }
////                        }
////                        //Console.WriteLine("[SpreadingProgress.Statuses]");
////                    }

////                    cutTicket.SpreadProgressList[n_s].SpreadingProgress.StepProgressList[COLORID].StepPliesSpread = cutTicket.SpreadList[COLORID].PliesSpread;

////                    xml = SerializeCutTicket(cutTicket);
////                    File.WriteAllText(filename_i, xml);

////                    break;

////                case "IsSpread":

////                    cutTicket.TicketStatus = "InProgress";
////                    var spreadListIsSpread = cutTicket.SpreadList.ToList();
////                    cutTicket.SpreadList.ToList().Last().PliesSpread += Convert.ToByte(nteli_aggiungere);
////                    cutTicket.SpreadList = spreadListIsSpread.ToArray();
////                    cutTicket.SpreadProgressList.ToList().Last().SpreadingProgress.PliesSpread += Convert.ToByte(nteli_aggiungere);

////                    var spreadProgressListIsSpread = cutTicket.SpreadProgressList.ToList().Last().SpreadingProgress.Statuses.ToList();

////                    spreadProgressListIsSpread.Add(new CutTicketSpreadProgressSpreadingProgressStatus()
////                    {
////                        Value = "IsSpread",
////                        timeStamp = dt //timeStamp = "2019-07-18T17:21:06Z"//Convert.ToDateTime(
////                    });

////                    cutTicket.SpreadProgressList.ToList().Last().SpreadingProgress.Statuses = spreadProgressListIsSpread.ToArray();
////                    cutTicket.SpreadList = spreadListIsSpread.ToArray();

////                    xml = SerializeCutTicket(cutTicket);

////                    File.WriteAllText(filename_i, xml);

////                    //creo barcode dopo
////                    NodeBarcodeCutTicket(JobFile, BarcodePath, file_i);

////                    break;

////                default:
////                    // --
////                    break;
////            }
////        }

        private static void NodeBarcodeCutTicket(String JobFile, string filename_b, string ActualCutTicket)
        {
            XmlElement root = null;
            XmlDeclaration xmldecl = null;
            XmlDocument doc_nc = new XmlDocument();
            //<? xml version = "1.0" encoding = "utf-8" ?>
            doc_nc.LoadXml("<CutTicket>" +
             "<Version>1.0</Version>" +
             "<JobFile>" + JobFile.Trim() + "</JobFile>" +
             "<ActualCutTicket>" + ActualCutTicket + "</ActualCutTicket></CutTicket>");

            xmldecl = doc_nc.CreateXmlDeclaration("1.0", null, null);
            xmldecl.Encoding = "UTF-8";
            root = doc_nc.DocumentElement;
            doc_nc.InsertBefore(xmldecl, root);
            doc_nc.Save(filename_b);
        }

        private static void CreateNodeSpreadProgressListDeparture(string FileName_i, String FileName_o, String Barcode, String[] StepColor, String[] n_telai, String ID, String PliesSpread, string n_teli, int nxSpreadProgressList)
        {
            //normalizzare file (lo fa la rutine prima e me lo scrive qua)//espande i tag file. //espandere tag
            XDocument _doc = null;
            String dt;

            int n_colori = StepColor.Count(); //Caso semplice // this.countS("SpreadList/Spread");

            //LOAD
            _doc = XDocument.Load(FileName_i);

            //Aggiornare cut ticket status -> in progress (se è già in progress)
            _doc.XPathSelectElement("CutTicket/TicketStatus").Value = "InProgress";

            dt = DateTime.Now.ToString("yyyy-MM-dd") + 'T' + DateTime.Now.ToString("hh:mm:ss") + 'Z';

            //TODO eventualmente prima creo spredprogress poi da inserire.

            if (nxSpreadProgressList == 0)//scrivo per la prima volta.
            {
                _doc = XDocument.Load(FileName_i);
                var srcTree =
                    new XElement("SpreadProgressList",
                    new XElement("SpreadProgress",
                    new XElement("SpreadingProgress",
                    new XElement("ID", ID),
                    new XElement("PliesSpread", PliesSpread),//pliesspread conincide con n_teli
                    new XElement("Statuses",
                    new XElement("Status", "SpreadingInProgress", new XAttribute("timeStamp", dt))),
                    //new XElement("Status", "IsSpread", new XAttribute("timeStamp", "2021-01-26T09:10:10Z"))),
                    new XElement("StepProgressList",
                    new XElement("StepProgress",
                    new XElement("StepId", "0"),//Caso Multicolor: indice array spread[] a cui fa rif. colore.
                    new XElement("StepColor", StepColor[0]),//Caso Multicolor: preddispongo array
                    new XElement("StepPliesSpread", PliesSpread))), //partenza del primo, parziale, se secondo salvarsi il totale.
                    new XElement("BarCodeCutTicket", Barcode))));
                _doc.XPathSelectElement("CutTicket/SpreadList").AddAfterSelf(srcTree);
            }
            else
            {
                var srcTree =
                   new XElement("SpreadProgress",
                   new XElement("SpreadingProgress",
                   new XElement("ID", ID),
                   new XElement("PliesSpread", n_teli),// 0 o 1 PliesSpread
                   new XElement("Statuses",
                   new XElement("Status", "SpreadingInProgress", new XAttribute("timeStamp", dt))),
                   new XElement("StepProgressList",
                   new XElement("StepProgress",
                   new XElement("StepId", "0"),
                   new XElement("StepColor", StepColor[0]),//StepColor[0] =White
                   new XElement("StepPliesSpread", PliesSpread))), //partenza del primo, parziale, se secondo salvarsi il totale.
                   new XElement("BarCodeCutTicket", Barcode))); //todo da spostare ->is spread
                _doc.XPathSelectElement("CutTicket/SpreadProgressList").Add(srcTree);
            }
            _doc.Save(FileName_o, SaveOptions.None);
            _doc = XDocument.Load(FileName_i);//ho fatto così eventualmente da prevedere riempimento multicolore prima e ripresa.
            // PATH //CutTicket / SpreadProgressList / SpreadProgress / SpreadingProgress / Statuses

            //TODO spostare su.
            //Caso Multicolor
            //int N = StepColor.Count();
            for (int i = 1; i < n_colori; i++)//n_colori
            {
                var srcTreeStepProgress =
                new XElement("StepProgress",
                new XElement("StepId", i.ToString()),        //parte da 0 e va su....
                new XElement("StepColor", StepColor[i].ToString()),
                new XElement("StepPliesSpread", n_telai[i]));   //da parametro passare il n riferito al colore.
                //Console.WriteLine($"CutTicket/SpreadProgressList/SpreadProgress/SpreadingProgress/StepProgressList[" + (nxSpreadProgressList+1) + "]/StepProgress");
                _doc.XPathSelectElement($"CutTicket/SpreadProgressList/SpreadProgress[" + (nxSpreadProgressList + 1) + "]/SpreadingProgress/StepProgressList/StepProgress[" + i.ToString() + "]").AddAfterSelf(srcTreeStepProgress);
            }

            _doc.XPathSelectElement("CutTicket/TicketStatus").Value = "InProgress";
            _doc.XPathSelectElement("CutTicket/SpreadList/Spread/PliesSpread").Value = PliesSpread;
            _doc.Save(FileName_o, SaveOptions.None);

        }//---CreateNodespreadProgressListPartenza
    }//---
}

//EOF