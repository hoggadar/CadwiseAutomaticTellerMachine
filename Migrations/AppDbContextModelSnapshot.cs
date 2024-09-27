﻿// <auto-generated />
using CadwiseAutomaticTellerMachine.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CadwiseAutomaticTellerMachine.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CadwiseAutomaticTellerMachine.MVVM.Models.BanknoteModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Denomination")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Banknotes");
                });

            modelBuilder.Entity("CadwiseAutomaticTellerMachine.MVVM.Models.CardModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Cash")
                        .HasColumnType("integer");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("character varying(16)");

                    b.Property<string>("PIN")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("character varying(4)");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("CadwiseAutomaticTellerMachine.MVVM.Models.StorageModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BanknoteId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BanknoteId");

                    b.ToTable("Storages");
                });

            modelBuilder.Entity("CadwiseAutomaticTellerMachine.MVVM.Models.TransactionLogModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<int>("CardId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.ToTable("TransactionLogs");
                });

            modelBuilder.Entity("CadwiseAutomaticTellerMachine.MVVM.Models.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CadwiseAutomaticTellerMachine.MVVM.Models.CardModel", b =>
                {
                    b.HasOne("CadwiseAutomaticTellerMachine.MVVM.Models.UserModel", "User")
                        .WithMany("Cards")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CadwiseAutomaticTellerMachine.MVVM.Models.StorageModel", b =>
                {
                    b.HasOne("CadwiseAutomaticTellerMachine.MVVM.Models.BanknoteModel", "Banknote")
                        .WithMany("Storages")
                        .HasForeignKey("BanknoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Banknote");
                });

            modelBuilder.Entity("CadwiseAutomaticTellerMachine.MVVM.Models.TransactionLogModel", b =>
                {
                    b.HasOne("CadwiseAutomaticTellerMachine.MVVM.Models.CardModel", "Card")
                        .WithMany("TransactionLogs")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");
                });

            modelBuilder.Entity("CadwiseAutomaticTellerMachine.MVVM.Models.BanknoteModel", b =>
                {
                    b.Navigation("Storages");
                });

            modelBuilder.Entity("CadwiseAutomaticTellerMachine.MVVM.Models.CardModel", b =>
                {
                    b.Navigation("TransactionLogs");
                });

            modelBuilder.Entity("CadwiseAutomaticTellerMachine.MVVM.Models.UserModel", b =>
                {
                    b.Navigation("Cards");
                });
#pragma warning restore 612, 618
        }
    }
}
