using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using ProRob.Extensions.Json;
using ProRob.Extensions.String;

namespace Machine
{
    public sealed partial class Localization
    {
        private static volatile bool isInitialized = false;
        public static bool IsInitialized => isInitialized;

        private static volatile MachineLanguage machineLanguage = MachineLanguage.English;
        public static MachineLanguage MachineLanguage
        {
            get => machineLanguage;
            set
            {
                lock (locker)
                {
                    machineLanguage = value;
                }
            }
        }

        private static readonly List<Dictionary<string, string>> dictionary = new List<Dictionary<string, string>>();

        private static Localization instance;

        private static readonly Object locker = new Object();

        static Localization()
        {
            var localization = new Localization();
        }

        private Localization()
        {
            //--
        }

        public static void Initialize(string dictionaryPath)
        {
            lock (locker)
            {
                if (!isInitialized)
                {
                    var localizationData = new LocalizationData().FromJsonFile(dictionaryPath);
                    dictionary.AddRange(localizationData.Dictionary);
                    isInitialized = true;

                    Console.WriteLine($"[Localization] initialized ({DateTime.UtcNow})");
                }
            }
        }

        public static string GetTranslation([CallerMemberName] string resource = "")
        {
            if (!IsInitialized || dictionary.Count == 0)
                return Fallback(resource);

            lock (locker)
            {
                if (TryGet(resource, MachineLanguage, out var translation) ||
                    TryGet(resource, MachineLanguage.English, out translation) ||
                    TryGet(resource, MachineLanguage.Italiano, out translation))
                {
                    return translation;
                }

                return Fallback(resource);
            }

        }

        private static bool TryGet(string key, MachineLanguage lang, out string translation)
        {
            try
            {
                translation = dictionary[(int)lang].TryGetValue(key, out var value) ? value : null;
                return translation != null;
            }
            catch
            {
                translation = null;
                return false;
            }
        }
        private static string Fallback(string resource)
        {
            return resource.Contains(" ") ? resource : resource.ToSentenceCase();
        }

        public static Localization Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new Localization();
                        }
                    }
                }

                return instance;
            }
        }
    }
}
