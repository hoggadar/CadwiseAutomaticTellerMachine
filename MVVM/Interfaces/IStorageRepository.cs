using CadwiseAutomaticTellerMachine.Business.DTOs;
using CadwiseAutomaticTellerMachine.MVVM.Models;

namespace CadwiseAutomaticTellerMachine.MVVM.Interfaces
{
    public interface IStorageRepository : IRepository<StorageModel>
    {
        Task<int> GetStorageBalance();
        Task<List<BanknoteQuantityDto>> GetBanknoteQuantity();
        Task UpdateBanknotesQuantity(List<BanknoteQuantityDto> banknotesQuantity);
    }
}
