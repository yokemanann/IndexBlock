using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IndexBlock.Models
{
    public class Transactions
    {
        [Key]
        [Column("transactionID", TypeName = "int(20)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }

        [Column("blockID", TypeName = "int(20)")]
        public int BlockId { get; set; }

        [Column("hash", TypeName = "varchar(66)")]
        public string? Hash { get; set; }

        [Column("from", TypeName = "varchar(42)")]
        public string? From { get; set; }

        [Column("to", TypeName = "varchar(42)")]
        public string? To { get; set; } 
        [Column("value", TypeName = "decimal(50, 0)")]
        public decimal Value { get; set; }
        [Column("gas", TypeName = "decimal(50, 0)")]
        public decimal Gas { get; set; }
        [Column("gasPrice", TypeName = "decimal(50, 0)")]
        public decimal GasPrice { get; set; }
        [Column("transactionIndex", TypeName = "int(20)")]
        public int TransactionIndex { get; set; }

        [ForeignKey(nameof(BlockId))]
        [InverseProperty("Transactions")]
        public virtual Blocks Block { get; set; }
    }
}
