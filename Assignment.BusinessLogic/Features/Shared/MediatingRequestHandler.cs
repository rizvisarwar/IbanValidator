using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment.BusinessLogic.Features.Shared
{
    public abstract class MediatingRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : class
    {
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
