using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IndexBlock.Models
{
    public class Blocks
    {
        [Key]
        [Column("blockID", TypeName = "int(20)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BlockId { get; set; }
        [Column("blockNumber", TypeName = "int(20)")]
        public int BlockNumber { get; set; }

        [Column("hash", TypeName = "varchar(66)")]
        public string Hash { get; set; }
        [Column("parentHash", TypeName = "varchar(66)")]
        public string ParentHash { get; set; }
        [Column("miner", TypeName = "varchar(42)")]
        public string Miner { get; set; }
        [Column("blockReward", TypeName = "decimal(50, 0)")]
        public decimal BlockReward { get; set; }
        [Column("gasLimit", TypeName = "decimal(50, 0)")]
        public decimal GasLimit { get; set; }
        [Column("gasUsed", TypeName = "decimal(50, 0)")]
        public decimal GasUsed { get; set; }
        [InverseProperty(nameof(Models.Transactions.Block))]
        public virtual ICollection<Transactions> Transactions { get; set; }
    }
}
