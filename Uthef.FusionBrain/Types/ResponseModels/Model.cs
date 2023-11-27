using System.Text.Json.Serialization;

namespace Uthef.FusionBrain.Types.ResponseModels
{
    public class Model
    {
        public int Id { get; }
        public string Name { get; }
        public float Version { get; }
        public string Type { get; }

        [JsonIgnore]
        public string VersionAsString { get; }


        [JsonConstructor]
        public Model(int id, string name, float version, string type)
        {
            Id = id;
            Name = name;
            Version = version;
            Type = type;
            VersionAsString = $"{Version:0.0}";
        }

        public Model(int id)
        {
            Id = id;
            Name = "";
            Version = 1.0f;
            Type = "";
            VersionAsString = $"{Version:0.0}";
        }
    }
}
