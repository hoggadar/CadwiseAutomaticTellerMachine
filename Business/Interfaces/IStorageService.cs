using CadwiseAutomaticTellerMachine.Business.DTOs;
using CadwiseAutomaticTellerMachine.MVVM.Models;

namespace CadwiseAutomaticTellerMachine.Business.Interfaces
{
    public interface IStorageService
    {
        Task<int> GetStorageBalance();
        Task<List<BanknoteQuantityDto>> GetBanknoteQuantity();

        Task<List<BanknoteQuantityDto>> WithdrawMoneyBig(int moneyRequest);
        //Task<List<BanknoteQuantityDto>> WithdrawSmall(int moneyRequest);
        Task IncreaseBanknoteQuantity(List<BanknoteQuantityDto> banknotesQuantity);
    }
}
