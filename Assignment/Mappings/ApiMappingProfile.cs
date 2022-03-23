using Assignment.Api.Models.Requests;
using Assignment.BusinessLogic.Features.Iban.ValidateIban;
using AutoMapper;

namespace Assignment.Api.Mappings
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<ValidationRequest, ValidateIbanRequest>()
                .ForMember(dest => dest.CorrelationId, opt => opt.Ignore());
        }
    }
}
