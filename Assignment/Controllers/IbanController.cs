using Assignment.Api.Models.Requests;
using Assignment.BusinessLogic.Features.Iban.ValidateIban;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Assignment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IbanController : MediatingController
    {
        protected IMapper _mapper { get; set; }

        public IbanController(IMediator mediator, IMapper mapper) : base(mediator)
        {
            _mapper = mapper;
        }

        [HttpPost("ValidateIban")]
        public async Task<IActionResult> ValidateIban([FromBody] ValidationRequest request)
        {
            var validateIbanRequest = _mapper.Map<ValidateIbanRequest>(request);
            return await HandleRequestAsync<ValidateIbanRequest, ValidateIbanResponse>(validateIbanRequest);
        }
    }
}
