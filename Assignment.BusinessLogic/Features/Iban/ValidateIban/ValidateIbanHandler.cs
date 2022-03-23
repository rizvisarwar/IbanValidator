using Assignment.BusinessLogic.Features.Shared;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment.BusinessLogic.Features.Iban.ValidateIban
{
    public class ValidateIbanHandler : MediatingRequestHandler<ValidateIbanRequest, ValidateIbanResponse>
    {
        private IConfiguration _configuration;

        public ValidateIbanHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override async Task<ValidateIbanResponse> Handle(ValidateIbanRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return new ValidateIbanResponse() { IsValid = true };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
