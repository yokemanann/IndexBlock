﻿// <auto-generated />
using IndexBlock.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IndexBlock.Migrations
{
    [DbContext(typeof(IndexBlock_DBContext))]
    partial class IndexBlock_DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("IndexBlock.Models.Blocks", b =>
                {
                    b.Property<int>("BlockId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(20)")
                        .HasColumnName("blockID");

                    b.Property<int>("BlockNumber")
                        .HasColumnType("int(20)")
                        .HasColumnName("blockNumber");

                    b.Property<decimal>("BlockReward")
                        .HasColumnType("decimal(50, 0)")
                        .HasColumnName("blockReward");

                    b.Property<decimal>("GasLimit")
                        .HasColumnType("decimal(50, 0)")
                        .HasColumnName("gasLimit");

                    b.Property<decimal>("GasUsed")
                        .HasColumnType("decimal(50, 0)")
                        .HasColumnName("gasUsed");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("varchar(66)")
                        .HasColumnName("hash");

                    b.Property<string>("Miner")
                        .IsRequired()
                        .HasColumnType("varchar(42)")
                        .HasColumnName("miner");

                    b.Property<string>("ParentHash")
                        .IsRequired()
                        .HasColumnType("varchar(66)")
                        .HasColumnName("parentHash");

                    b.HasKey("BlockId");

                    b.ToTable("Blocks");
                });

            modelBuilder.Entity("IndexBlock.Models.Transactions", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(20)")
                        .HasColumnName("transactionID");

                    b.Property<int>("BlockId")
                        .HasColumnType("int(20)")
                        .HasColumnName("blockID");

                    b.Property<string>("From")
                        .HasColumnType("varchar(42)")
                        .HasColumnName("from");

                    b.Property<decimal>("Gas")
                        .HasColumnType("decimal(50, 0)")
                        .HasColumnName("gas");

                    b.Property<decimal>("GasPrice")
                        .HasColumnType("decimal(50, 0)")
                        .HasColumnName("gasPrice");

                    b.Property<string>("Hash")
                        .HasColumnType("varchar(66)")
                        .HasColumnName("hash");

                    b.Property<string>("To")
                        .HasColumnType("varchar(42)")
                        .HasColumnName("to");

                    b.Property<int>("TransactionIndex")
                        .HasColumnType("int(20)")
                        .HasColumnName("transactionIndex");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(50, 0)")
                        .HasColumnName("value");

                    b.HasKey("TransactionId");

                    b.HasIndex("BlockId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("IndexBlock.Models.Transactions", b =>
                {
                    b.HasOne("IndexBlock.Models.Blocks", "Block")
                        .WithMany("Transactions")
                        .HasForeignKey("BlockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Block");
                });

            modelBuilder.Entity("IndexBlock.Models.Blocks", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
