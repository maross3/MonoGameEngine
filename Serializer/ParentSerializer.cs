using System;
using TestMonogame.Serializer;

namespace BotSerializer
{
    public class ParentSerializer
    {
        private readonly Deserializer _deserializer;
        private readonly Serializer _serializer;
        private SerializationMethod _serializationMethod;

        public ParentSerializer(SerializationMethod serializationMethod)
        {
            _serializationMethod = serializationMethod;
            _deserializer = new Deserializer();
            _serializer = new Serializer();
        }

        public void ChangeSerializationMethod(SerializationMethod newSerializationMethod) => 
            _serializationMethod = newSerializationMethod;

        #region Serialization
        public bool Serialize<T>(T obj) =>
            _serializationMethod switch
            {
                SerializationMethod.Json => _serializer.JsonSerialize(obj),
                _ => throw new ArgumentOutOfRangeException(nameof(_serializationMethod), _serializationMethod, 
                    "Invalid serialization parameter.")
            };
        public bool Serialize<T>(T obj, string path) =>
            _serializationMethod switch
            {
                SerializationMethod.Json => _serializer.JsonSerialize(obj, path),
                _ => throw new ArgumentOutOfRangeException(nameof(_serializationMethod), _serializationMethod, 
                    "Invalid serialization parameter.")
            };
        #endregion

        #region Deserialization
        public T Deserialize<T>() =>
            _serializationMethod switch
            {
                SerializationMethod.Json => Deserializer.JsonDeserialize<T>(),
                _ => throw new ArgumentOutOfRangeException(nameof(_serializationMethod), _serializationMethod,
                    "Invalid Deserialization parameter.")
            };

        public T Deserialize<T>(string path) =>
            _serializationMethod switch
            {
                SerializationMethod.Json => Deserializer.JsonDeserialize<T>(path),
                _ => throw new ArgumentOutOfRangeException(nameof(_serializationMethod), _serializationMethod,
                    "Invalid Deserialization parameter.")
            };
        #endregion

    }
}
