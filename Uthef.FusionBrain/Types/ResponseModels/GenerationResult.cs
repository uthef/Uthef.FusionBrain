using System.Text.Json.Serialization;

namespace Uthef.FusionBrain.Types.ResponseModels;

public class GenerationResult
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<string>? Files { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool Censored { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ErrorDescription { get; }

    public GenerationResult(IEnumerable<string>? files, bool censored, string errorDescription)
    {
        Files = files;
        Censored = censored;
        ErrorDescription = errorDescription;
    }
}