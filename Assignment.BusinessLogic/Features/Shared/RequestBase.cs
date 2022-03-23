using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Assignment.BusinessLogic.Features.Shared
{
    public abstract class RequestBase<TResponse> : IRequest<TResponse> where TResponse : class
    {
        [JsonIgnore]
        [IgnoreMap]
        public Guid? CorrelationId { get; set; }
    }
}
