using CadwiseAutomaticTellerMachine.MVVM.Models;
using System.IO.Packaging;

namespace CadwiseAutomaticTellerMachine.Business.Interfaces
{
    public interface ICardService
    {
        Task<CardModel?> GetById(int id);
        Task<CardModel?> GetByNumber(string number);
        Task<CardModel?> GetByUserId(int userId);
        Task Update(CardModel card);
        Task IncreaseCash();
    }
}
