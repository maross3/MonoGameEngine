using System.IO;
using System.Runtime.Serialization;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace BotSerializer
{
    internal class Serializer : SerializationNames
    {
        #region Json
        public bool JsonSerialize<T>(T obj)
        {
            try
            {
                var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
                using var writer = File.CreateText(JSON_FILE_NAME);
                writer.Write(json);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool JsonSerialize<T>(T obj, string path)
        {
            try
            {
                var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
                using var writer = File.CreateText(path);
                writer.Write(json);

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
