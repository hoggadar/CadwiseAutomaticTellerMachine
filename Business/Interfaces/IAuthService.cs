using CadwiseAutomaticTellerMachine.MVVM.DTOs;
using CadwiseAutomaticTellerMachine.MVVM.Models;

namespace CadwiseAutomaticTellerMachine.Business.Interfaces
{
    public interface IAuthService
    {
        UserModel? CurrentUser { get; }
        Task<bool> AuthUser(AuthDto dto);
    }
}
