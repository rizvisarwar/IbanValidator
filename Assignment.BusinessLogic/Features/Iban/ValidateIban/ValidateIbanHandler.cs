using Assignment.BusinessLogic.Features.Shared;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment.BusinessLogic.Features.Iban.ValidateIban
{
    public class ValidateIbanHandler : MediatingRequestHandler<ValidateIbanRequest, ValidateIbanResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly string validLetters;
        public ValidateIbanHandler(IConfiguration configuration)
        {
            _configuration = configuration;
            validLetters = _configuration.GetSection("ValidLetters").Value;
        }

        public override async Task<ValidateIbanResponse> Handle(ValidateIbanRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = IsValidIban(request.Iban);

                return await Task.FromResult(new ValidateIbanResponse() { IsValid = response });
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool IsValidIban(string iban)
        {
            var countryCode = iban.Substring(0, 2);
            var hasCorrectLength = HasCorrectLength(countryCode.ToUpper(), iban.Length);
            if (!hasCorrectLength) return false;

            var swappedString = iban.Substring(4, iban.Length - 4) + iban.Substring(0, 4);
            var replacedString = ReplaceLettersWithNumbers(swappedString);

            decimal.TryParse(replacedString, out decimal ibanNumber);
                      
            return ibanNumber % 97 == 1;
        }

        private bool HasCorrectLength(string countryCode, int inputLength)
        {
            int.TryParse(_configuration.GetSection($"IbanLength:{countryCode}").Value, out var length);

            return (length == inputLength);
        }

        private string ReplaceLettersWithNumbers(string input)
        {
            var result = string.Empty;
            foreach (char letter in input)
            {
                var index = validLetters.IndexOf(letter);
                result += (index != -1) ? (index + 10).ToString() : letter.ToString();
            }

            return result;
        }
    }
}
