using CadwiseAutomaticTellerMachine.MVVM.Models;
using Microsoft.EntityFrameworkCore;

namespace CadwiseAutomaticTellerMachine.Infrastructure.Data
{
    public class DataSeeder
    {
        private readonly AppDbContext _context;

        public DataSeeder(AppDbContext context)
        {
            _context = context;
        }

        public async Task SeedUsers()
        {
            if (_context.Users.Any()) return;
            var users = new List<UserModel>
            {
                new UserModel { Name = "Андрей", Surname = "Ермоленко" },
                new UserModel { Name = "Александр", Surname = "Иванов" },
                new UserModel { Name = "Николай", Surname = "Сухов" },
            };
            await _context.Users.AddRangeAsync(users);
            await _context.SaveChangesAsync();
        }

        public async Task SeedCards()
        {
            if (_context.Cards.Any()) return;
            var users = await _context.Users.ToListAsync();
            var cards = new List<CardModel>
            {
                new CardModel { Number = "1234567812345678", PIN = "1234", Cash = 1000, UserId = users[0].Id },
                new CardModel { Number = "8765432187654321", PIN = "5678", Cash = 2000, UserId = users[0].Id },
                new CardModel { Number = "2345678923456789", PIN = "2345", Cash = 1500, UserId = users[1].Id },
                new CardModel { Number = "9876543298765432", PIN = "6789", Cash = 2500, UserId = users[1].Id },
                new CardModel { Number = "3456789034567890", PIN = "3456", Cash = 1800, UserId = users[2].Id },
                new CardModel { Number = "0987654309876543", PIN = "7890", Cash = 3000, UserId = users[2].Id }
            };
            await _context.Cards.AddRangeAsync(cards);
            await _context.SaveChangesAsync();
        }

        public async Task SeedBanknotes()
        {
            if (_context.Banknotes.Any()) return;
            var banknotes = new List<BanknoteModel>
            {
                new BanknoteModel { Denomination = 10 },
                new BanknoteModel { Denomination = 50 },
                new BanknoteModel { Denomination = 100 },
                new BanknoteModel { Denomination = 200 },
                new BanknoteModel { Denomination = 500 },
                new BanknoteModel { Denomination = 1000 },
                new BanknoteModel { Denomination = 2000 },
                new BanknoteModel { Denomination = 5000 },
            };
            await _context.Banknotes.AddRangeAsync(banknotes);
            await _context.SaveChangesAsync();
        }

        public async Task SeedStorages()
        {
            if (_context.Storages.Any()) return;
            var banknotes = _context.Banknotes.ToList();
            var storages = banknotes.Select(banknote => new StorageModel
            {
                BanknoteId = banknote.Id,
                Quantity = 50
            }).ToList();
            await _context.Storages.AddRangeAsync(storages);
            await _context.SaveChangesAsync();
        }

        public async Task SeedAllData()
        {
            await SeedUsers();
            await SeedCards();
            await SeedBanknotes();
            await SeedStorages();
        }
    }
}
