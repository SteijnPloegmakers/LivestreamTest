using System.Text.Json;
using RestSharp;

namespace LivestreamTest.Logic
{
    public class CloudflareStreamService
    {
        private readonly string _apiKey;
        private readonly string _accountId;
        private readonly RestClient _client;

        public CloudflareStreamService(IConfiguration configuration)
        {
            _apiKey = configuration["Cloudflare:ApiKey"];
            _accountId = configuration["Cloudflare:AccountId"];
            _client = new RestClient($"https://api.cloudflare.com/client/v4/accounts/{_accountId}/stream");
        }

        public async Task<string> CreateLiveInput(string name)
        {
            var request = new RestRequest("live_inputs", Method.Post);
            request.AddHeader("Authorization", $"Bearer {_apiKey}");
            request.AddJsonBody(new { name });

            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                var content = JsonDocument.Parse(response.Content);
                var streamId = content.RootElement.GetProperty("result").GetProperty("uid").GetString();
                return streamId; // Return the unique ID of the created live input
            }
            throw new Exception($"Failed to create live input: {response.Content}");
        }
    }
}
