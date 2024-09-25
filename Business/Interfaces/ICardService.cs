using CadwiseAutomaticTellerMachine.MVVM.Models;
using System.IO.Packaging;

namespace CadwiseAutomaticTellerMachine.Business.Interfaces
{
    public interface ICardService
    {
        Task<CardModel?> GetById(int id);
        Task<CardModel?> GetByNumber(string number);
    }
}
