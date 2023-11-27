using System.Text.Json.Serialization;
using Uthef.FusionBrain.Serialization;
using Uthef.FusionBrain.Types.ResponseModels;
using Uthef.FusionBrain.Utils;

namespace Uthef.FusionBrain.Types
{
    public class Prompt : Serializable<Prompt>
    {
        public string Type { get; } = "GENERATE";

        [JsonPropertyName("numImages")]
        public int NumberOfImages { get; } = 1;

        [JsonIgnore]
        public Model Model { get; set; }

        [JsonIgnore]
        public Size ImageSize { get; set; } = Size.FromWidth(1024, OptimalRatios.R1_1);

        public int Width => ImageSize.Width;
        public int Height => ImageSize.Height;

        [JsonIgnore]
        public string Query { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? NegativePromptUnclip { get; set; }

        [JsonInclude]
        public readonly Dictionary<string, string> GenerateParams = new();

        [JsonIgnore]
        public Style? Style { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("style")]
        public string? StyleName => Style?.Name;

        public Prompt(Model model, string query)
        {
            Model = model;
            Query = query;

            GenerateParams.Add("query", query);
        }
    }
}
