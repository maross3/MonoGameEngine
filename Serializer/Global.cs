namespace BotSerializer
{
    public class SerializationNames
    {
        public const string JSON_FILE_NAME = @"dat.json";
        public const string REGISTRY_SUB_KEY = @"SOFTWARE\DATA";
        public const string REGISTRY_VALUE = @"DATA";
        public const string CONTRACT_FILE_NAME = @"dat.xml";
    }

    public enum SerializationMethod
    {
        Json,
        Registry,
        Xml
    }
}
