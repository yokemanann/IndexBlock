using IndexBlock.Common.Extensions;
using IndexBlock.Contracts;
using IndexBlock.Models;
using IndexBlock.Repositories.Base;
using IndexBlock.Services;
using Microsoft.Extensions.Logging;

namespace IndexBlock
{
    public class App
    {
        private readonly IRepository<Blocks> _blocksRepository;
        private readonly IRepository<Transactions> _transactionsRepository;
        private readonly IEthRpcJsonService _alchemyService;
        private readonly ILogger _logger;

        public App(
            IRepository<Blocks> blocksRepository,
            IRepository<Transactions> transactionsRepository,
            IEthRpcJsonService alchemyService,
            ILogger<App> logger)
        {
            _blocksRepository = blocksRepository;
            _transactionsRepository = transactionsRepository;
            _alchemyService = alchemyService;
            _logger = logger;
        }

        public async Task Run(string[] args)
        {
            try
            {
                var startBlock = 12100001;
                var endBlock = 12100500;
                var size = (endBlock - startBlock) + 1;
                Task[] taskArray = new Task[size];
                for (int i = 0; i < size; i++)
                {
                    await ProcessBlocks(startBlock + i);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Message :{e.Message}");
            }
        }

        private async Task ProcessBlocks(int blockNo)
        {
            _logger.LogInformation($"ProcessBlocks - Block number {blockNo}");
            try
            {
                var block = await _alchemyService.eth_getBlockByNumber(blockNo);

                if (block is null)
                {
                    _logger.LogInformation($"Block number {blockNo} does not exist");
                    return;
                }   

                var blockId = await InsertBlock(block);
                var transactionCount = await _alchemyService.eth_getBlockTransactionCountByNumber(blockNo);

                if (transactionCount == 0)
                {
                    _logger.LogInformation($"Block number {blockNo} has no transactions");
                    return;
                }                    

                for (int i = 0; i < transactionCount; i++)
                {
                    var transaction = await _alchemyService.eth_getTransactionByBlockNumberAndIndex(blockNo, i);
                    await InsertTransaction(transaction, blockId, i);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Message : {e.Message}");
                _logger.LogError($"Error - ProcessBlocks - Block number {blockNo}");
            }
        }

        private async Task<int> InsertBlock(Block block)
        {
            _logger.LogInformation($"InsertBlock - Block number {block.Number.ToNumberInt()}");
            var newBlock = new Blocks
            {
                BlockNumber = block.Number.ToNumberInt(),
                Hash = block.Hash,
                ParentHash = block.ParentHash,
                Miner = block.Miner,
                //BlockReward = block.BlockReward, //Unable to get this value from Alchemy
                GasLimit = block.GasLimit.ToNumberLong(),
                GasUsed = block.GasUsed.ToNumberLong()
            };
            await _blocksRepository.AddAsync(newBlock);
            await _blocksRepository.SaveChangesAsync();
            return newBlock.BlockId;
        }

        private async Task InsertTransaction(Transaction transaction, int blockId, int index)
        {
            _logger.LogInformation($"InsertTransaction - Block Id {blockId}, Index: {index}");
            await _transactionsRepository.AddAsync(
                new Transactions
                {
                    BlockId = blockId,
                    Hash = transaction.Hash,
                    From = transaction.From,
                    To = transaction.To,
                    Value = (decimal)transaction.Value.ToNumberBigInteger(),
                    Gas = transaction.Gas.ToNumberLong(),
                    GasPrice = transaction.GasPrice.ToNumberLong(),
                    TransactionIndex = index,
                }
            );
            await _transactionsRepository.SaveChangesAsync();
        }
    }
}
