using Assignment.BusinessLogic.Features.Iban.ValidateIban;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Assignment.UnitTests
{
    public class IbanValidatorUnitTests
    {
        private IConfiguration _configuration;
        private ValidateIbanRequest validIban;
        private ValidateIbanRequest invalidIban;

        public IbanValidatorUnitTests()
        {
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration
                .Setup(_ => _.GetSection("IbanLength:GB").Value)
                .Returns("22");
            mockConfiguration
               .Setup(_ => _.GetSection("ValidLetters").Value)
               .Returns("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            _configuration = mockConfiguration.Object;

            validIban = new ValidateIbanRequest() { Iban = "GB82WEST12345698765432" };
            invalidIban = new ValidateIbanRequest() { Iban = "GB82" };
        }

        [Fact]
        public async Task ReturnsTrueForValidIban()
        {
            var handler = new ValidateIbanHandler(_configuration);

            var response = await handler.Handle(validIban, new CancellationToken()).ConfigureAwait(false);

            Assert.True(response.IsValid);
        }

        [Fact]
        public async Task ReturnsFalseForInvalidIban()
        {
            var handler = new ValidateIbanHandler(_configuration);

            var response = await handler.Handle(invalidIban, new CancellationToken()).ConfigureAwait(false);

            Assert.True(!response.IsValid);
        }
    }
}
