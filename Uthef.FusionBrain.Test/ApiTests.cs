using System.Text.Json;
using Uthef.FusionBrain.Types;
using Uthef.FusionBrain.Extensions;

namespace Uthef.FusionBrain.Test
{
    public class ApiTests
    {
        public AuthCredentials Credentials { get; }

        public FusionBrainApi Api { get; }

        public const string FilePath = "Credentials.json";

        public ApiTests()
        {
            if (!File.Exists(FilePath)) throw new($"\"{FilePath}\" not found");

            using var fs = new FileStream(FilePath, FileMode.Open);

            Credentials = JsonSerializer.Deserialize<AuthCredentials>(fs) ?? throw new($"Unable to parse \"{FilePath}\"");

            Api = new(Credentials);
        }

        [SetUp]
        public void Setup() { }


        [Test]
        public async Task CheckAvailability()
        {
            var models = await Api.GetModelsAsync();
            Assert.That(models.Any(), Is.True);
            var availability = await Api.CheckServiceAvailability(models.First());
            
            Assert.That(availability.Status.Length > 0);
        }
        
        [Test]
        public async Task GetStyles()
        {
            var styles = await Api.GetStylesAsync();
            Assert.That(styles.Any(), Is.True);

            foreach (var style in styles)
            {
                Console.WriteLine(style.Name);
            }
        }

        [Test]
        public async Task GenerateImage()
        {
            var models = await Api.GetModelsAsync();
            Assert.That(models.Count(), Is.GreaterThan(0));

            var prompt = new Prompt(models.First(), "bird");

            var firstStatus = await Api.GenerateAsync(prompt);
            Assert.That(firstStatus.Status is Status.Initial);

            var finalStatus = await Api.PollAsync(firstStatus.Uuid, callback: status => {
                Console.WriteLine($"{status.Uuid} - {status.Status}");
            });

            if (finalStatus.Failed)
            {
                Console.WriteLine(finalStatus.Result?.ErrorDescription);
            }

            var bytes = finalStatus.GetFirstImageBytes();
            Assert.That(finalStatus.Result, Is.Not.Null);
            Assert.That(bytes, Is.Not.Null);

            int counter = 0;    
            
            foreach (var base64Image in finalStatus.Result!.Files!)
            {
                await File.WriteAllBytesAsync($"{finalStatus.Uuid}-{counter++}.jpg", Convert.FromBase64String(base64Image));
            }
        }
    }
}