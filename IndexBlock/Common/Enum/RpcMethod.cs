
using System.ComponentModel;

namespace IndexBlock.Common.Enum
{
    public enum RpcMethod
    {
        [Description("eth_getBlockByNumber")]
        eth_getBlockByNumber,
        [Description("eth_getBlockTransactionCountByNumber")]
        eth_getBlockTransactionCountByNumber,
        [Description("eth_getTransactionByBlockNumberAndIndex")]
        eth_getTransactionByBlockNumberAndIndex
    }
}