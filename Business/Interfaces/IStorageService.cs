using CadwiseAutomaticTellerMachine.Business.DTOs;
using CadwiseAutomaticTellerMachine.MVVM.Models;

namespace CadwiseAutomaticTellerMachine.Business.Interfaces
{
    public interface IStorageService
    {
        Task<int> GetStorageBalance();
        Task<List<BanknoteQuantityDto>> GetBanknoteQuantity();

        Task<List<BanknoteQuantityDto>> WithdrawMoneyBig(int moneyRequest);
        Task<List<BanknoteQuantityDto>> WithdrawMoneySmall(int moneyRequest);
        int ProcessWithdraw(List<BanknoteQuantityDto> denominations, int request, List<BanknoteQuantityDto> result);
        Task ValidateMoneyRequest(int moneyRequest);
        Task IncreaseBanknoteQuantity(List<BanknoteQuantityDto> banknotesQuantity);
    }
}
