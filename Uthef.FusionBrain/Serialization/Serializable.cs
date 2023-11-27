using System.Text.Json;

namespace Uthef.FusionBrain.Serialization
{
    public class Serializable<T>
    {
        internal string Serialize()
        {
            var jsonString = JsonSerializer.Serialize(this, typeof(T), new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return jsonString;
        }
    }
}
