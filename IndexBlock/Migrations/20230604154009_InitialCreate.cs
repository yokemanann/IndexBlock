using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IndexBlock.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Blocks",
                columns: table => new
                {
                    blockID = table.Column<int>(type: "int(20)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    blockNumber = table.Column<int>(type: "int(20)", nullable: false),
                    hash = table.Column<string>(type: "varchar(66)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    parentHash = table.Column<string>(type: "varchar(66)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    miner = table.Column<string>(type: "varchar(42)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    blockReward = table.Column<decimal>(type: "decimal(50,0)", nullable: false),
                    gasLimit = table.Column<decimal>(type: "decimal(50,0)", nullable: false),
                    gasUsed = table.Column<decimal>(type: "decimal(50,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blocks", x => x.blockID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    transactionID = table.Column<int>(type: "int(20)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    blockID = table.Column<int>(type: "int(20)", nullable: false),
                    hash = table.Column<string>(type: "varchar(66)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    from = table.Column<string>(type: "varchar(42)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    to = table.Column<string>(type: "varchar(42)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value = table.Column<decimal>(type: "decimal(50,0)", nullable: false),
                    gas = table.Column<decimal>(type: "decimal(50,0)", nullable: false),
                    gasPrice = table.Column<decimal>(type: "decimal(50,0)", nullable: false),
                    transactionIndex = table.Column<int>(type: "int(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.transactionID);
                    table.ForeignKey(
                        name: "FK_Transactions_Blocks_blockID",
                        column: x => x.blockID,
                        principalTable: "Blocks",
                        principalColumn: "blockID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_blockID",
                table: "Transactions",
                column: "blockID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Blocks");
        }
    }
}
