using System.Collections.Generic;

using BattleCards.Models.Cards;
using BattleCards.Models.Users;

namespace BattleCards.Services
{
    public interface IValidator
    {
        ICollection<string> ValidateUserRegistration(RegisterUserFormModel model);

        ICollection<string> ValidateCardCreation(AddCardViewModel model);

        //ICollection<string> ValidateCommitCreation(CreateCommitForModel model);
    }
}
