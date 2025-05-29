using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;

using ProRob.Extensions.Json;
using ProRob.Extensions.String;

namespace Machine
{
    [Synchronization()]
    public sealed partial class Localization
    {
        private static volatile bool isInitialized = false;
        public static bool IsInitialized => isInitialized;

        private static volatile MachineLanguage machineLanguage = MachineLanguage.English;
        public static MachineLanguage MachineLanguage { get => machineLanguage; set => machineLanguage = value; }

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
            var localizationData = new LocalizationData().FromJsonFile(dictionaryPath);
            dictionary.AddRange(localizationData.Dictionary);

            isInitialized = true;

            Console.WriteLine($"[Localization] initialized ({DateTime.UtcNow})");
        }

        public static string GetTranslation([CallerMemberName] string resource = "")
        {
            if (IsInitialized)
            {
                string translation = String.Empty;

                if (dictionary[(int)Localization.MachineLanguage].TryGetValue(resource, out translation))
                {
                    return translation;
                }
                else if (dictionary[(int)MachineLanguage.English].TryGetValue(resource, out translation))
                {
                    return translation;
                }
                else if (dictionary[(int)MachineLanguage.Italiano].TryGetValue(resource, out translation))
                {
                    return translation;
                }
                else
                {
                    if (resource.Contains(" "))
                    {
                        return resource;
                    }
                    else
                    {
                        return resource.ToSentenceCase();
                    }
                }
            }
            else
            {
                return resource;
            }
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
