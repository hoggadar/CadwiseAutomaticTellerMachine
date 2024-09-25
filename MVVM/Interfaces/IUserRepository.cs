using CadwiseAutomaticTellerMachine.MVVM.Models;

namespace CadwiseAutomaticTellerMachine.MVVM.Interfaces
{
    public interface IUserRepository : IRepository<UserModel>
    {
        Task<UserModel?> GetByCardId(int cardId);
    }
}
