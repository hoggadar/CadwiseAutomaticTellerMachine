using CadwiseAutomaticTellerMachine.Business.DTOs;
using CadwiseAutomaticTellerMachine.MVVM.Models;

namespace CadwiseAutomaticTellerMachine.Business.Interfaces
{
    public interface IStorageService
    {
        Task<int> GetStorageBalance();
        Task<List<BanknoteQuantityDto>> GetBanknoteQuantity();

        Task<List<BanknoteQuantityDto>> WithdrawMoneyBig(int moneyRequest);
        //Task<Dictionary<BanknoteModel, int>> WithdrawSmall(int amount);
    }
}
