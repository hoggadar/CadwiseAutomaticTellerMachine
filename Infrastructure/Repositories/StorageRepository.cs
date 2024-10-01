using CadwiseAutomaticTellerMachine.Business.DTOs;
using CadwiseAutomaticTellerMachine.Infrastructure.Data;
using CadwiseAutomaticTellerMachine.MVVM.Interfaces;
using CadwiseAutomaticTellerMachine.MVVM.Models;
using Microsoft.EntityFrameworkCore;

namespace CadwiseAutomaticTellerMachine.Infrastructure.Repositories
{
    public class StorageRepository : Repository<StorageModel>, IStorageRepository
    {
        public StorageRepository(AppDbContext context) : base(context) { }

        public async Task<int> GetStorageBalance()
        {
            return await _context.Storages
                .Include(x => x.Banknote)
                .Select(x => x.Banknote!.Denomination * x.Quantity)
                .SumAsync();
        }

        public async Task<List<BanknoteQuantityDto>> GetBanknoteQuantity()
        {
           return await _context.Storages
                .Include(x => x.Banknote)
                .Select(x => new BanknoteQuantityDto
                {
                    Denomination = x.Banknote!.Denomination,
                    Quantity = x.Quantity,
                })
                .OrderByDescending(x => x.Denomination)
                .ToListAsync();
        }

        public async Task UpdateBanknotesQuantity(List<BanknoteQuantityDto> banknotesQuantity)
        {
            foreach (var banknoteQuantity in banknotesQuantity)
            {
                var storage = await _context.Storages
                    .Include(x => x.Banknote)
                    .FirstOrDefaultAsync(x => x.Banknote!.Denomination == banknoteQuantity.Denomination);

                var diff = storage!.Quantity - banknoteQuantity.Quantity;

                if (storage != null && diff >= 0)
                {
                    storage.Quantity -= banknoteQuantity.Quantity;
                    await Update(storage);
                }
                else
                {
                    throw new InvalidOperationException($"Ошибка изменения количества купюр в банкомате");
                }
            }
        }

        public async Task IncreaseBanknoteQuantity(List<BanknoteQuantityDto> banknotesQuantity)
        {
            foreach(var banknoteQuantity in banknotesQuantity)
            {
                var storage = await _context.Storages
                   .Include(x => x.Banknote)
                   .FirstOrDefaultAsync(x => x.Banknote!.Denomination == banknoteQuantity.Denomination);

                if (storage != null)
                {
                    int maxAdditionalQuantity = 100 - storage.Quantity;
                    int quantityToAdd = Math.Min(banknoteQuantity.Quantity, maxAdditionalQuantity);
                    storage.Quantity += quantityToAdd;
                    await Update(storage);
                } 
            }
        }
    }
}
