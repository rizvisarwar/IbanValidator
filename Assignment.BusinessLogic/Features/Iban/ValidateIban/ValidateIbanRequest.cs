using Assignment.BusinessLogic.Features.Shared;

namespace Assignment.BusinessLogic.Features.Iban.ValidateIban
{
    public class ValidateIbanRequest : RequestBase<ValidateIbanResponse>
    {
        public string Iban { get; set; }
    }   
}
