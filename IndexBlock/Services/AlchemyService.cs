using IndexBlock.Common.Enum;
using IndexBlock.Common.Extensions;
using IndexBlock.Contracts;
using IndexBlock.Services.Client;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Numerics;

namespace IndexBlock.Services
{
    public class AlchemyService : IEthRpcJsonService
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IEthClient _ethClient;
        private readonly ILogger<AlchemyService> _logger;

        public AlchemyService(
            IHttpClientFactory httpClientFactory,
            IEthClient ethClient,
            ILogger<AlchemyService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _ethClient = ethClient;
            _logger = logger;
        }

        public async Task<Block?> eth_getBlockByNumber(int blockNumber)
        {
            _logger.LogInformation($"eth_getBlockByNumber - blockNumber: {blockNumber}");

            var rpcRequest = new RpcRequest(RpcMethod.eth_getBlockByNumber);
            rpcRequest.AddParam(((BigInteger)blockNumber).ToHexString());
            rpcRequest.AddParam(false);

            var responseResult = await _ethClient.PostAsync(_httpClientFactory, rpcRequest);

            if (responseResult.Error is null)
            {
                var json = JsonConvert.SerializeObject(responseResult.Result);
                var block = JsonConvert.DeserializeObject<Block>(json);
                return block;
            }

            _logger.LogInformation($"Error - eth_getBlockByNumber - blockNumber: {blockNumber}, Message: {responseResult.Error.Message}");
            return null;
        }

        public async Task<int> eth_getBlockTransactionCountByNumber(int blockNumber)
        {
            _logger.LogInformation($"eth_getBlockTransactionCountByNumber - blockNumber: {blockNumber}");

            var rpcRequest = new RpcRequest(RpcMethod.eth_getBlockTransactionCountByNumber);
            rpcRequest.AddParam(blockNumber.ToHexString());

            var responseResult = await _ethClient.PostAsync(_httpClientFactory, rpcRequest);

            if (responseResult.Error is null)
            {
                var count = Convert.ToInt32(responseResult.Result, 16);
                return count;
            }

            _logger.LogInformation($"Error - eth_getBlockTransactionCountByNumber - blockNumber: {blockNumber}, Message: {responseResult.Error.Message}");            
            return 0;
        }

        public async Task<Transaction?> eth_getTransactionByBlockNumberAndIndex(int blockNumber, int index)
        {
            _logger.LogInformation($"eth_getBlockTransactionCountByNumber - blockNumber: {blockNumber}, index: {index}");

            var rpcRequest = new RpcRequest(RpcMethod.eth_getTransactionByBlockNumberAndIndex);
            rpcRequest.AddParam(blockNumber.ToHexString());
            rpcRequest.AddParam(index.ToHexString());

            var responseResult = await _ethClient.PostAsync(_httpClientFactory, rpcRequest);

            if (responseResult.Error is null)
            {
                var json = JsonConvert.SerializeObject(responseResult.Result);
                var transaction = JsonConvert.DeserializeObject<Transaction>(json);
                return transaction;
            }

            _logger.LogInformation($"Error - eth_getTransactionByBlockNumberAndIndex - blockNumber: {blockNumber}, index: {index}, Message: {responseResult.Error.Message}");
            return null;
        }
    }
}
