using Assignment.BusinessLogic.Features.Delivery.DeliveryDates;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Assignment.UnitTests
{
    public class DeliveryDatesTests
    {
        private IConfiguration _configuration;

        public DeliveryDatesTests()
        {
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration
                .Setup(_ => _.GetSection("UpcomingDays").Value)
                .Returns("14");
            mockConfiguration
               .Setup(_ => _.GetSection("GreenDeliveryDay").Value)
               .Returns("3");
            mockConfiguration
               .Setup(_ => _.GetSection("TemporaryProductDays").Value)
               .Returns("7");
            _configuration = mockConfiguration.Object;
        }

        [Fact]
        public async Task ResponseDoesnotHaveWeekend()
        {
            var handler = new ValidateIbanHandler(_configuration);
            var postalCode = "12345";
            var request = GetDeliveryDatesRequest(postalCode);

            var response = await handler.Handle(request, new CancellationToken()).ConfigureAwait(false);
            bool result = ResponseHasWeekend(response);

            Assert.False(result, "Response should not have weekend");
        }

        private bool ResponseHasWeekend(List<ValidateIbanResponse> response)
        {
            return response.Any(x => x.DeliveryDate.DayOfWeek.Equals(DayOfWeek.Saturday) || x.DeliveryDate.DayOfWeek.Equals(DayOfWeek.Sunday));
        }

        private ValidateIbanRequest GetDeliveryDatesRequest(string postalCode)
        {
            var product = GetNormalProduct();

            return new ValidateIbanRequest()
            {
                PostalCode = postalCode,
                Products = new List<ProductInfo> { product }
            };
        }

        private ProductInfo GetNormalProduct()
        {
            return new ProductInfo()
            {
                Name = "testProduct",
                DeliveryDays = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday },
                ProductId = 1,
                ProductType = BusinessLogic.Enums.ProductType.Normal,
                DaysInAdvance = 2
            };
        }
    }
}
