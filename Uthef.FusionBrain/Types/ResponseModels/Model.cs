using System.Text.Json.Serialization;

namespace Uthef.FusionBrain.Types.ResponseModels
{
    public class Model
    {
        public Guid Id { get; }
        public string Name { get; }
        public string NameEn { get; }
        public string Description { get; }
        public string DescriptionEn { get; }
        public string[] Tags { get; }
        public string Status { get; }
        public DateTime CreatedDate { get; }
        public DateTime LastModified { get; }
        public string ImagePreview { get; }
        public float Version { get; }
        public string Type { get; }

        [JsonIgnore]
        public string VersionAsString { get; }


        [JsonConstructor]
        public Model(Guid id, string name, string nameEn, string description, string descriptionEn, string[] tags, 
            string status, DateTime createdDate, DateTime lastModified, string imagePreview, float version, string type)
        {
            Id = id;
            Name = name;
            NameEn = nameEn;
            Description = description;
            DescriptionEn = descriptionEn;
            Tags = tags;
            Status = status;
            CreatedDate = createdDate;
            LastModified = lastModified;
            ImagePreview = imagePreview;
            Version = version;
            Type = type;
            VersionAsString = $"{Version:0.0}";
        }

        public Model(Guid id)
        {
            Id = id;
            Name = "";
            NameEn = "";
            Description = "";
            DescriptionEn = "";
            Tags = [];
            Status = "";
            CreatedDate = DateTime.MinValue;
            LastModified = DateTime.MinValue;
            ImagePreview = "";
            Version = 1.0f;
            Type = "";
            VersionAsString = $"{Version:0.0}";
        }
    }
}
