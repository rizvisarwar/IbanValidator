using Microsoft.AspNetCore.Http;
using System;

namespace Assignment.BusinessLogic.Extensions
{
    public static class HttpRequestHeaderExtensions
    {
        /// <summary>
        /// Generate new Correlation Id Header if it doesn't exist or its invalid
        /// </summary>
        /// <param name="headerDictionary"></param>
        public static void EnsureCorrelationIdHeader(this IHeaderDictionary headerDictionary)
        {
            if (!headerDictionary.Keys.Contains(Constants.HttpHeaderConstants.CorrelationIdHeaderKey))
            {
                var correlationId = Guid.NewGuid();
                headerDictionary.Add(Constants.HttpHeaderConstants.CorrelationIdHeaderKey, correlationId.ToString());
            }
            else if (!Guid.TryParse(headerDictionary[Constants.HttpHeaderConstants.CorrelationIdHeaderKey], out var parsedGuid) || parsedGuid == Guid.Empty)
            {
                var correlationId = Guid.NewGuid();
                headerDictionary[Constants.HttpHeaderConstants.CorrelationIdHeaderKey] = correlationId.ToString();
            }
        }

        /// <summary>
        /// Get Correlation Id from Header
        /// </summary>
        /// <param name="headerDictionary"></param>
        /// <returns></returns>
        public static Guid GetCorrelationIdFromHeader(this IHeaderDictionary headerDictionary)
        {
            if (headerDictionary.Keys.Contains(Constants.HttpHeaderConstants.CorrelationIdHeaderKey))
            {
                Guid.TryParse(headerDictionary[Constants.HttpHeaderConstants.CorrelationIdHeaderKey], out var correlationId);
                return correlationId;
            }

            return Guid.Empty;
        }
    }
}
