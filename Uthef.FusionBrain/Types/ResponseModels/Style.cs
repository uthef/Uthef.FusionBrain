using System.Text.Json.Serialization;

namespace Uthef.FusionBrain.Types.ResponseModels
{
    public class Style
    {
        public string Name { get; }
        public string Title { get; } = "";
        public string TitleEn { get; } = "";
        public string Image { get; } = "";

        [JsonIgnore]
        public Uri ImageUri => new(Image);

        [JsonConstructor]
        public Style(string name, string title, string titleEn, string image)
        {
            Name = name;
            Title = title;
            TitleEn = titleEn;
            Image = image;
        }

        public Style(string name)
        {
            Name = name;
        }
    }
}