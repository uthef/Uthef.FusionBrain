# Fusion Brain API
Fusion Brain is a free AI image generation service.

Check out the official documentation: https://fusionbrain.ai/docs/en/doc/api-dokumentaciya/

## Usage example
```cs
// using Uthef.FusionBrain;
// using Uthef.FusionBrain.Types;
// using Uthef.FusionBrain.Extensions;

var credentials = new AuthCredentials("API_KEY", "SECRET_KEY");
var api = new FusionBrainApi(credentials);

var models = await api.GetModelsAsync();
var prompt = new Prompt(models.First(), "bird");

var firstStatus = await api.GenerateAsync(prompt);

var finalStatus = await Api.PollAsync(firstStatus.Uuid);

if (finalStatus.Failed)
{
    Console.WriteLine(finalStatus.ErrorDescription);
}

var bytes = finalStatus.GetFirstImageBytes();

if (bytes is { })
{
    using var fs = new FileStream("image.png", FileMode.OpenOrCreate);
    fs.Write(bytes);
}
```
