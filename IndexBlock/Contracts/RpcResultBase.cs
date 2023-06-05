
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace IndexBlock.Contracts
{
    public class RpcResult
    {
        [JsonProperty(PropertyName = "id", Order = 0)]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "jsonrpc", Order = 1)]
        public decimal JsonRpc { get; set; }

        [JsonProperty(PropertyName = "result", Order = 2)]
        public dynamic Result { get; set; }

        [JsonProperty(PropertyName = "error", Order = 2)]
        public Error Error { get; set; }
    }

    public class Error
    {
        [JsonProperty(PropertyName = "code", Order = 0)]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "message", Order = 0)]
        public string Message { get; set; }

    }

}
