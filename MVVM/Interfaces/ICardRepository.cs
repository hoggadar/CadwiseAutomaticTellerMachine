using CadwiseAutomaticTellerMachine.MVVM.Models;

namespace CadwiseAutomaticTellerMachine.MVVM.Interfaces
{
    public interface ICardRepository : IRepository<CardModel>
    {
        Task<CardModel?> GetByNumber(string number);
        Task<CardModel?> GetByUserId(int userId);
    }
}
