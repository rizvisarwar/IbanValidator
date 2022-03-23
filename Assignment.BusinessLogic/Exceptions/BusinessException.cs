using System;
using System.Net;

namespace Assignment.BusinessLogic.Exceptions
{
    public class BusinessException : Exception
    {
        public string Type { get; }

        public int? Code { get; }
        public Guid? CorrelationId { get; }

        public HttpStatusCode? HttpStatusCode { get; }
                
        public BusinessException(Guid? correlationId, string message, HttpStatusCode? httpStatusCode = null, string type = null) : base(message)
        {
            HttpStatusCode = httpStatusCode;
            Type = type;
            CorrelationId = correlationId;
        }
    }
}
