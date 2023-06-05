
using IndexBlock.Contracts;

namespace IndexBlock.Services.Client
{
    public interface IEthClient
    {
        Task<RpcResult> PostAsync(IHttpClientFactory httpClientFactory, RpcRequest rpcRequest);
    }
}
