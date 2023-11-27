namespace Uthef.FusionBrain.Types
{
    public class AuthCredentials
    {
        public string ApiKey { get; }
        public string SecretKey { get; }

        internal string ApiKeyHeaderValue => $"Key {ApiKey}";
        internal string SecretKeyHeaderValue => $"Secret {SecretKey}";

        public AuthCredentials(string apiKey, string secretKey)
        {
            ApiKey = apiKey;
            SecretKey = secretKey;
        }
    }
}
