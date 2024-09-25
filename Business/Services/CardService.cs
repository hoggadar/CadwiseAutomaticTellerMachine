using CadwiseAutomaticTellerMachine.Business.Interfaces;
using CadwiseAutomaticTellerMachine.MVVM.Interfaces;
using CadwiseAutomaticTellerMachine.MVVM.Models;

namespace CadwiseAutomaticTellerMachine.Business.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;

        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task<CardModel?> GetById(int id)
        {
            return await _cardRepository.GetById(id);
        }

        public async Task<CardModel?> GetByNumber(string number)
        {
            return await _cardRepository.GetByNumber(number);
        }
    }
}
