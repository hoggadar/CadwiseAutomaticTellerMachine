using CadwiseAutomaticTellerMachine.Business.Interfaces;
using CadwiseAutomaticTellerMachine.MVVM.Interfaces;
using CadwiseAutomaticTellerMachine.MVVM.Models;

namespace CadwiseAutomaticTellerMachine.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModel?> GetById(int id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task<UserModel?> GetByCardId(int cardId)
        {
            return await _userRepository.GetByCardId(cardId);
        }
    }
}
