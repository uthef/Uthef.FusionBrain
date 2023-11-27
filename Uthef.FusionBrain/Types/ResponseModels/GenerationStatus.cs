using System.Text.Json.Serialization;
using Uthef.FusionBrain.Types;

namespace Uthef.FusionBrain.Types.ResponseModels
{
    public class GenerationStatus
    {
        public Status Status { get; }
        public Guid Uuid { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<string>? Images { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool Censored { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ErrorDescription { get; }

        [JsonIgnore]
        public bool Completed => Status == Status.Done && Images is { };

        [JsonIgnore]
        public bool Failed => Status == Status.Fail;

        public GenerationStatus(Status status, Guid uuid, IEnumerable<string>? images, bool censored, string? errorDescription)
        {
            Status = status;
            Uuid = uuid;
            Images = images;
            Censored = censored;
            ErrorDescription = errorDescription;
        }

        public byte[]? GetFirstImageBytes()
        {
            if (!Completed || !Images!.Any()) return null;

            return Convert.FromBase64String(Images!.First());
        }

    }
}
