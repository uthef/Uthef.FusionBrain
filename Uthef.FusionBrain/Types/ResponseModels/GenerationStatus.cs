using System.Text.Json.Serialization;
using Uthef.FusionBrain.Types;

namespace Uthef.FusionBrain.Types.ResponseModels
{
    public class GenerationStatus
    {
        public Status Status { get; }
        public Guid Uuid { get; }
        
        public GenerationResult? Result { get; } 

        [JsonIgnore] public bool Completed => Status == Status.Done && Result is { };

        [JsonIgnore]
        public bool Failed => Status == Status.Fail;

        public GenerationStatus(Status status, Guid uuid, GenerationResult? result)
        {
            Status = status;
            Uuid = uuid;
            Result = result;
        }

        public byte[]? GetFirstImageBytes()
        {
            if (!Completed || Result?.Files is null || !Result.Files.Any()) return null;

            return Convert.FromBase64String(Result.Files.First());
        }

    }
}
