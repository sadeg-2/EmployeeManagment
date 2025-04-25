
using System.Text.Json;

namespace Client.Library.Helpers
{
    public static class Serializations
    {
        public static string SerializeObj<T>(T modelObject) => JsonSerializer.Serialize(modelObject);
        public static T? DeSerializeJsonString<T>(string jsonString) => JsonSerializer.Deserialize<T>(jsonString);

        public static IList<T>? DeserializeStringJsonStringList<T>(string jsonString) =>
            JsonSerializer.Deserialize<IList<T>>(jsonString);

    }
}
