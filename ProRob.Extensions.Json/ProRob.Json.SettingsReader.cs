using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

using ProRob.Extensions.Object;

namespace ProRob
{
    public partial class Json
    {
        public interface ISettingsReader
        {
            T Load<T>() where T : class;
            T LoadSection<T>() where T : class;
            object Load(Type type);
            object LoadSection(Type type);
            object StoreSection(Type type, object obj);
        }

        public class SettingsReader : ISettingsReader
        {
            private readonly string configurationFilePath;
            private readonly string sectionNameSuffix;

            private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                ContractResolver = new SettingsReaderContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            public SettingsReader(string configurationFilePath, string sectionNameSuffix = "Settings")
            {
                this.configurationFilePath = configurationFilePath;
                this.sectionNameSuffix = sectionNameSuffix;
            }

            public T Load<T>() where T : class => Load(typeof(T)) as T;

            public T LoadSection<T>() where T : class => LoadSection(typeof(T)) as T;

            public object Load(Type type)
            {
                if (!File.Exists(configurationFilePath))
                {
                    return Activator.CreateInstance(type);
                }

                var jsonFile = File.ReadAllText(configurationFilePath);

                return JsonConvert.DeserializeObject(jsonFile, type, JsonSerializerSettings);
            }

            public object LoadSection(Type type)
            {
                if (!File.Exists(configurationFilePath))
                {
                    return Activator.CreateInstance(type);
                }

                Type itemType = type;
                if (type.IsGenericType && type.GetGenericTypeDefinition()
                        == typeof(List<>))
                {
                    itemType = type.GetGenericArguments()[0];
                }

                var jsonFile = File.ReadAllText(configurationFilePath);
                var section = itemType.Name;
                var settingsData = JsonConvert.DeserializeObject<dynamic>(jsonFile, JsonSerializerSettings);
                var settingsSection = settingsData[section];

                return settingsSection == null ? Activator.CreateInstance(type) : JsonConvert.DeserializeObject(JsonConvert.SerializeObject(settingsSection, Formatting.Indented), type, JsonSerializerSettings);
            }

            public object StoreSection(Type type, object o)
            {
                if (!File.Exists(configurationFilePath))
                {
                    return Activator.CreateInstance(type);
                }

                Type itemType = type;
                if (type.IsGenericType && type.GetGenericTypeDefinition()
                        == typeof(List<>))
                {
                    itemType = type.GetGenericArguments()[0];
                }

                var jsonFile = File.ReadAllText(configurationFilePath);
                var section = itemType.Name;
                var settingsData = JsonConvert.DeserializeObject<dynamic>(jsonFile, JsonSerializerSettings);
                settingsData[section] = JToken.FromObject(o);
                var serializedSettingsData = JsonConvert.SerializeObject(settingsData, Formatting.Indented);
                File.WriteAllText(configurationFilePath, serializedSettingsData);

                return JsonConvert.DeserializeObject(settingsData[section].ToString(), type);
            }

            private class SettingsReaderContractResolver : DefaultContractResolver
            {
                protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
                {
                    var props = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                        .Select(p => CreateProperty(p, memberSerialization))
                        .Union(type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                        .Select(f => CreateProperty(f, memberSerialization)))
                        .ToList();

                    props.ForEach(p =>
                    {
                        p.Writable = true;
                        p.Readable = true;
                    });

                    return props;
                }
            }
        }
    }
}
