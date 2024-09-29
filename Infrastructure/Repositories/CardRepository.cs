using CadwiseAutomaticTellerMachine.Infrastructure.Data;
using CadwiseAutomaticTellerMachine.MVVM.Interfaces;
using CadwiseAutomaticTellerMachine.MVVM.Models;
using Microsoft.EntityFrameworkCore;

namespace CadwiseAutomaticTellerMachine.Infrastructure.Repositories
{
    public class CardRepository : Repository<CardModel>, ICardRepository
    {
        public CardRepository(AppDbContext context) : base(context) { }

        public async Task<CardModel?> GetByNumber(string number)
        {
            return await _context.Set<CardModel>().FirstOrDefaultAsync(x => x.Number == number);
        }

        public async Task<CardModel?> GetByUserId(int userId)
        {
            return await _context.Set<CardModel>().FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task IncreaseCash()
        {
            var cards = await GetAll();
            foreach (var card in cards) card.Cash += 17850;
            _context.Cards.UpdateRange(cards);
            await _context.SaveChangesAsync();
        }
    }
}
