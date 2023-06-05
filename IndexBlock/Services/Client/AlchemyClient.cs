using IndexBlock.Common.Config;
using IndexBlock.Contracts;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Configuration;
using System.Text;

namespace IndexBlock.Services.Client
{
    public class AlchemyClient : IEthClient
    {
        private readonly AlchemyAPI _alchemyApiConfig;
        private readonly ILogger _logger;

        public AlchemyClient(
            IOptions<AlchemyAPI> alchemyApiConfig,
            ILogger<AlchemyClient> logger)
        {
            _alchemyApiConfig = alchemyApiConfig.Value;
            _logger = logger;

            if (string.IsNullOrEmpty(_alchemyApiConfig.APIKey))
            {
                _logger.LogError("Alchemy API Key is not set");
                throw new SettingsPropertyNotFoundException();
            }
        }

        public async Task<RpcResult> PostAsync(IHttpClientFactory httpClientFactory, RpcRequest rpcRequest)
        {
            using var httpClient = httpClientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(rpcRequest), Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await httpClient.PostAsync($"{_alchemyApiConfig.Endpoint}{_alchemyApiConfig.APIKey}", content);
            string responseBody = await response.Content.ReadAsStringAsync();

            var responseResult = JsonConvert.DeserializeObject<RpcResult>(responseBody);

            return responseResult;
        }
    }
}
