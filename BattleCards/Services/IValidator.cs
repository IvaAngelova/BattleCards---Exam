using System.Collections.Generic;

using BattleCards.Models.Users;

namespace BattleCards.Services
{
    public interface IValidator
    {
        ICollection<string> ValidateUserRegistration(RegisterUserFormModel model);

        //ICollection<string> ValidateRepositoryCreation(CreateRepositoryForModel model);

        //ICollection<string> ValidateCommitCreation(CreateCommitForModel model);
    }
}
