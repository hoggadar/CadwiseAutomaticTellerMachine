using CadwiseAutomaticTellerMachine.MVVM.Models;
using Microsoft.EntityFrameworkCore;

namespace CadwiseAutomaticTellerMachine.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<CardModel> Cards { get; set; }
        public DbSet<TransactionLogModel> TransactionLogs { get; set; }
        public DbSet<BanknoteModel> Banknotes { get; set; }
        public DbSet<ATMModel> ATMs { get; set; }

        public AppDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=atm;Username=admin;Password=admin");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
