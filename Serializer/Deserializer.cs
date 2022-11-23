using System;
using System.IO;
using BotSerializer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TestMonogame.Serializer
{
    internal class Deserializer : SerializationNames
    {
        #region Json
        public static T JsonDeserialize<T>()
        {
            try
            {
                using var file = File.OpenText(JSON_FILE_NAME);
                using var reader = new JsonTextReader(file);

                var jobj = (JObject)JToken.ReadFrom(reader);
                return JsonConvert.DeserializeObject<T>(jobj.ToString())!;

            }
            catch
            {
                throw new Exception("Failed to deserialize json file.");
            }
        }
        public static T JsonDeserialize<T>(string path)
        {
            try
            {
                using var file = File.OpenText(path);
                using var reader = new JsonTextReader(file);

                var jobj = (JObject)JToken.ReadFrom(reader);
                return JsonConvert.DeserializeObject<T>(jobj.ToString());

            }
            catch
            {
                throw new Exception("Failed to deserialize json file." + path);
            }
        }
        #endregion
    }
}
