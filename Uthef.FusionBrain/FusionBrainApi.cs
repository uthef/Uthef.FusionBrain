using System.Collections.Immutable;
using System.Net.Http.Json;
using System.Text;
using Uthef.FusionBrain.Exceptions;
using Uthef.FusionBrain.Types;
using Uthef.FusionBrain.Types.ResponseModels;

namespace Uthef.FusionBrain
{
    public class FusionBrainApi
    {
        private readonly HttpClient _httpClient;
        public readonly AuthCredentials Credentials;
        public readonly Uri BaseUri;

        public FusionBrainApi(AuthCredentials credentials, HttpClient? httpClient = null, Uri? baseUri = null) 
        {
            Credentials = credentials;
            _httpClient = httpClient ?? new HttpClient();
            BaseUri = baseUri ?? new("https://api-key.fusionbrain.ai/key/api/v1/");

            if (httpClient is { })
            {
                _httpClient.DefaultRequestHeaders.Remove("X-Key");
                _httpClient.DefaultRequestHeaders.Remove("X-Secret");
            }

            _httpClient.DefaultRequestHeaders.Add("X-Key", Credentials.ApiKeyHeaderValue);
            _httpClient.DefaultRequestHeaders.Add("X-Secret", Credentials.SecretKeyHeaderValue);
        }

        public async Task<IEnumerable<Style>> GetStylesAsync(CancellationToken token = default)
        {
            var uri = new Uri("https://cdn.fusionbrain.ai/static/styles/api");
            var response = await _httpClient.GetAsync(uri, token);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Style>>(cancellationToken: token) ?? ImmutableList.Create<Style>();
        }

        public async Task<GenerationStatus> GenerateAsync(Prompt prompt, CancellationToken token = default)
        {
            var uri = GetFullUri("text2image/run");

            var request = new HttpRequestMessage(HttpMethod.Post, uri);

            var @params = new StringContent(prompt.Serialize(), Encoding.UTF8, "application/json");

            var multipartData = new MultipartFormDataContent
            {
                { new StringContent(prompt.Model.Id.ToString()), "model_id" },
                { @params, "params" }
            };

            request.Content = multipartData;

            var response = await _httpClient.SendAsync(request, token);
            response.EnsureSuccessStatusCode();

            var status = await response.Content.ReadFromJsonAsync<GenerationStatus>(cancellationToken: token);
            return status ?? throw new UnexpectedResponseException();
        }

        public async Task<IEnumerable<Model>> GetModelsAsync(CancellationToken token = default)
        {
            var uri = GetFullUri("models");

            var response = await _httpClient.GetAsync(uri, token);
            response.EnsureSuccessStatusCode();

            var models = await response.Content.ReadFromJsonAsync<IEnumerable<Model>>(cancellationToken: token);
            return models ?? throw new UnexpectedResponseException();
        }

        public async Task<GenerationStatus> CheckStatusAsync(Guid uuid, CancellationToken token = default)
        {
            var uri = GetFullUri($"text2image/status/{uuid}");

            var response = await _httpClient.GetAsync(uri, token);
            response.EnsureSuccessStatusCode();

            var status = await response.Content.ReadFromJsonAsync<GenerationStatus>(cancellationToken: token);
            return status ?? throw new UnexpectedResponseException();
        }

        private Uri GetFullUri(string relativeUrl)
        {
            var baseUri = BaseUri;
            var baseUrl = BaseUri.ToString();

            if (!baseUrl.EndsWith("/")) 
                baseUri = new Uri($"{baseUrl}/");

            return new(baseUri, relativeUrl);
        }
    }
}
