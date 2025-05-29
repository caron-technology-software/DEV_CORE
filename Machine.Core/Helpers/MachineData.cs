#undef SAVE_JSON

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Isam.Esent.Collections.Generic;

using ProRob;
using ProRob.Threading;

namespace Machine.Utility
{
    public class MachineData
    {
        private const int MaxElementsInQueue = 50;

        private static int MaxElementsProcessedContemporary = 0;
        private static int ElementsRead = 0;
        private static int ElementsWritten = 0;

        private static Counter counter = new Counter();

        private static PersistentDictionary<string, string> dictionary;

        private static volatile object objLocker = new object();

        public static bool IsInitialized { get; set; } = false;
        public static int Errors { get; private set; } = 0;

        public static void Initialize(string path)
        {
            dictionary = new PersistentDictionary<string, string>(path);

            IsInitialized = true;
        }

        private static string GetKeyName(string path)
        {
            return FileSystem.GetFilenameFromFullPath(path.Replace('.', '_'));
        }

        public static void Save<T>(string path, T item)
        {
            if (!IsInitialized)
            {
                return;
            }

            int n = counter.CurrentCount;

            if (n > MaxElementsInQueue)
            {
                return;
            }

            counter.Increment();

            if (n > MaxElementsProcessedContemporary)
            {
                MaxElementsProcessedContemporary = n;
            }

            try
            {
                string json = Json.Serialize(item);
                string key = GetKeyName(path);
#if SAVE_JSON
                File.WriteAllText(Path.Combine(Constants.Path.TmpFolder, $"save_{key}.json"), json);
#endif
                dictionary[key] = json;

                ElementsWritten++;
            }
            catch
            {
                Errors++;

                Console.WriteLine($"[MachineData] Errors: {Errors}");
            }
            finally
            {
                counter.Decrement();
            }
        }

        //GPIx1 per caricare ad esempio "read_spreader_working_context_db.json" da c:\caron\tmp folder:
//        public static void Save01<T>(string path, T item)
//        {
//            if (!IsInitialized)
//            {
//                return;
//            }

//            int n = counter.CurrentCount;

//            if (n > MaxElementsInQueue)
//            {
//                return;
//            }

//            counter.Increment();

//            if (n > MaxElementsProcessedContemporary)
//            {
//                MaxElementsProcessedContemporary = n;
//            }

//            try
//            {
//                //////string json = Json.Serialize(item);
//                string key = GetKeyName(path);
//                string json = File.ReadAllText(Path.Combine(Constants.Path.TmpFolder, $"read_{key}.json"));

//                //GPIx1 correzione Tubolar con Tubular:
//                json = json.Replace("Tubolar", "Tubular");

//#if SAVE_JSON
//                File.WriteAllText(Path.Combine(Constants.Path.TmpFolder, $"save_{key}.json"), json);
//#endif
//                dictionary[key] = json;

//                ElementsWritten++;
//            }
//            catch
//            {
//                Errors++;

//                Console.WriteLine($"[MachineData] Errors: {Errors}");
//            }
//            finally
//            {
//                counter.Decrement();
//            }
//        }
        //GPFx1

        public static bool ExistFile(string path)
        {
            bool containsKey = false;

            lock (objLocker)
            {
                string key = GetKeyName(path);
                containsKey = dictionary.ContainsKey(key);
            }

            return containsKey;
        }

        public static T Read<T>(string path) where T : new()
        {
            string key = GetKeyName(path);

            bool containsKey = false;

            lock (objLocker)
            {
                containsKey = dictionary.ContainsKey(key);
            }

            if (containsKey)
            {
                string json = string.Empty;

                lock (objLocker)
                {
                    json = dictionary[key];

                    //GPIx1 correzione Tubolar con Tubular:         //GPI27 cavato per conflitti con configurazione tubolare!!!:
                    //////json = json.Replace("Tubolar", "Tubular");
                    //GPFx1

                }
#if SAVE_JSON
                File.WriteAllText(Path.Combine(Constants.Path.TmpFolder, $"read_{key}.json"), json);
#endif
                ElementsRead++;

                var obj = Json.Deserialize<T>(json);

                if (obj is null || string.IsNullOrEmpty(json))
                {
                    Errors++;

                    ProConsole.WriteLine($"[MachineData] Key: \"{key}\" => null\n JSON: {json}", ConsoleColor.Cyan);
                }

                return obj;
            }
            else
            {
                Console.WriteLine($"[MachineData] Dictionary doesn't contains key \"{key}\".");

                return new T();
            }
        }

        public static void Close()
        {
            if (!IsInitialized)
            {
                return;
            }

            IsInitialized = false;

            Console.WriteLine($"[MachineData] Closing ({counter.CurrentCount} elements)..");

            counter.WaitValue(0);

            lock (objLocker)
            {
                dictionary.Flush();

                dictionary.Dispose();
            }

            Console.WriteLine($"[MachineData] Closed (E: {Errors} R:{ElementsRead} W:{ElementsWritten} M:{MaxElementsProcessedContemporary})");
        }

        public static void WaitAllOperations()
        {
            Console.WriteLine($"[MachineData] Waiting {counter.CurrentCount} elements..");
            counter.WaitValue(0);
        }
    }
}