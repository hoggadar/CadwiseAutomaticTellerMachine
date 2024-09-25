using CadwiseAutomaticTellerMachine.MVVM.Models;

namespace CadwiseAutomaticTellerMachine.Interfaces
{
    public interface IAuthService
    {
        UserModel CurrentUser { get; }
    }
}
