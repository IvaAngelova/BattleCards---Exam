using System.Linq;
using System.Collections.Generic;

using BattleCards.Data;
using BattleCards.Models.Users;
using BattleCards.Models.Cards;

namespace BattleCards.Services
{
    public class Validator : IValidator
    {
        public ICollection<string> ValidateCardCreation(AddCardViewModel model)
        {
            var errors = new List<string>();

            if (model.Name.Length < DataConstants.CardNameMinLength
                || model.Name.Length > DataConstants.CardNameMaxLength)
            {
                errors.Add($"Card name '{model.Name}' is not valid! It must be between {DataConstants.CardNameMinLength} and {DataConstants.CardNameMaxLength}.");
            }

            if (model.Attack < 0)
            {
                errors.Add($"Attack '{model.Attack}' can''t be a negative number");
            }

            if (model.Health < 0)
            {
                errors.Add($"Health '{model.Health}' can''t be a negative number");
            }

            if (model.Description.Length > DataConstants.DescriptionMaxLength)
            {
                errors.Add($"Description '{model.Description}' is not valid! Min Length must be {DataConstants.DescriptionMaxLength}");
            }

            return errors;
        }

        public ICollection<string> ValidateUserRegistration(RegisterUserFormModel model)
        {
            var errors = new List<string>();

            if (model.Username.Length < DataConstants.UsernameMinLength
                || model.Username.Length > DataConstants.UsernameMaxLength)
            {
                errors.Add($"Username '{model.Username}' is not valid! It must be between {DataConstants.UsernameMinLength} and {DataConstants.UsernameMaxLength}.");
            }

            if (model.Password.Length < DataConstants.PassowrdMinLength
                || model.Username.Length > DataConstants.PassowrdMaxLength)
            {
                errors.Add($"The provided password is not valid! It must be between {DataConstants.PassowrdMinLength} and {DataConstants.PassowrdMaxLength}.");
            }

            if (model.Password.Any(x => x == ' '))
            {
                errors.Add($"The provided password cannot contain whitespaces.");
            }

            if (model.Password != model.ConfirmPassword)
            {
                errors.Add($"Parssword and its confirmation are different.");
            }

            return errors;
        }
    }
}
