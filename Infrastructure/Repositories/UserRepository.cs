using CadwiseAutomaticTellerMachine.Infrastructure.Data;
using CadwiseAutomaticTellerMachine.MVVM.Interfaces;
using CadwiseAutomaticTellerMachine.MVVM.Models;
using Microsoft.EntityFrameworkCore;

namespace CadwiseAutomaticTellerMachine.Infrastructure.Repositories
{
    public class UserRepository : Repository<UserModel>, IUserRepository
    {
        private readonly ICardRepository _cardRepository;

        public UserRepository(AppDbContext context, ICardRepository cardRepository) : base(context)
        {
            _cardRepository = cardRepository;
        }

        public async Task<UserModel?> GetByCardId(int cardId)
        {
            var card = await _cardRepository.GetById(cardId);
            if (card == null) return null;
            return await _context.Set<UserModel>().FirstOrDefaultAsync(x => x.Id == card.UserId);
        }
    }
}
