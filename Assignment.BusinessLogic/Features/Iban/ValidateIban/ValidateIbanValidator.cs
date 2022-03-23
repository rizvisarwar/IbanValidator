using FluentValidation;

namespace Assignment.BusinessLogic.Features.Iban.ValidateIban
{    
    public sealed class ValidateIbanValidator : AbstractValidator<ValidateIbanRequest>
    {
        public ValidateIbanValidator()
        {
            RuleFor(r => r.Iban).NotEmpty().WithMessage("Iban is required");
        }         
    }    
}
