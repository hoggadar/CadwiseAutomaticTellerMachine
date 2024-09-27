using CadwiseAutomaticTellerMachine.Business.Interfaces;
using CadwiseAutomaticTellerMachine.MVVM.DTOs;
using CadwiseAutomaticTellerMachine.MVVM.Models;
using CadwiseAutomaticTellerMachine.MVVM.ViewModels;

namespace CadwiseAutomaticTellerMachine.Services
{
    public class AuthService : ObservableObject, IAuthService
    {
        private readonly ICardService _cardService;
        private readonly IUserService _userService;

        private UserModel? _currentUser;
        private CardModel? _currentCard;

        public AuthService(ICardService cardService, IUserService userService)
        {
            _cardService = cardService;
            _userService = userService;
        }

        public async Task<bool> AuthUser(AuthDto dto)
        {
            var card = await _cardService.GetByNumber(dto.CardNumber);
            if (card == null) return false;

            var user = await _userService.GetByCardId(card.Id);
            if (user == null) return false;

            if (dto.CardPIN == card.PIN)
            {
                CurrentUser = user;
                CurrentCard = card;
                return true;
            }

            CurrentUser = null;
            CurrentCard = null;

            return false;
        }

        public UserModel? CurrentUser
        {
            get => _currentUser;
            private set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

        public CardModel? CurrentCard
        {
            get => _currentCard;
            private set
            {
                _currentCard = value;
                OnPropertyChanged(nameof(CurrentCard));
            }
        }
    }
}
