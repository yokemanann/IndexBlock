using IndexBlock.Contracts;

namespace IndexBlock.Services
{
    public interface IEthRpcJsonService
    {
        Task<Block?> eth_getBlockByNumber(int blockNumber);
        Task<int> eth_getBlockTransactionCountByNumber(int blockNumber);
        Task<Transaction?> eth_getTransactionByBlockNumberAndIndex(int blockNumber, int index);
    }
}