using System.Text.Json.Serialization;

namespace Uthef.FusionBrain.Types
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Status
    {
        Initial,
        Processing,
        Done,
        Fail
    }
}
