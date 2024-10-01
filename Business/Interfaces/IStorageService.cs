using CadwiseAutomaticTellerMachine.Business.DTOs;
using CadwiseAutomaticTellerMachine.MVVM.Models;

namespace CadwiseAutomaticTellerMachine.Business.Interfaces
{
    public interface IStorageService
    {
        Task<int> GetStorageBalance();
        Task<List<BanknoteQuantityDto>> GetBanknoteQuantity();

        Task<List<BanknoteQuantityDto>> WithdrawMoneyBig(long moneyRequest);
        Task<List<BanknoteQuantityDto>> WithdrawMoneySmall(long moneyRequest);
        long ProcessWithdraw(List<BanknoteQuantityDto> denominations, long request, List<BanknoteQuantityDto> result);
        Task ValidateMoneyRequest(long moneyRequest);
        Task IncreaseBanknoteQuantity(List<BanknoteQuantityDto> banknotesQuantity);
    }
}
