using CadwiseAutomaticTellerMachine.MVVM.Models;

namespace CadwiseAutomaticTellerMachine.Business.Interfaces
{
    public interface IUserService
    {
        Task<UserModel?> GetById(int id);
        Task<UserModel?> GetByCardId(int cardId);
    }
}
