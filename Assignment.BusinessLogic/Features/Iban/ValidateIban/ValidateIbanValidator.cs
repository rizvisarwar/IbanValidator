using FluentValidation;
using System.Text.RegularExpressions;

namespace Assignment.BusinessLogic.Features.Iban.ValidateIban
{
    public sealed class ValidateIbanValidator : AbstractValidator<ValidateIbanRequest>
    {
        public ValidateIbanValidator()
        {
            RuleFor(r => r.Iban).NotEmpty().WithMessage("Iban is required");

            RuleFor(r => r.Iban).Must(x => DoesNotContainSpecialCharacters(x)).WithMessage("Iban should not contain special characters");
        }

        private bool DoesNotContainSpecialCharacters(string iban)
        {
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

            return regexItem.IsMatch(iban);
        }
    }
}
