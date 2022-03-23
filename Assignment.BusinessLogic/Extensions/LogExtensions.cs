using Microsoft.Extensions.Logging;
using System;

namespace Assignment.BusinessLogic.Extensions
{
    public static class LogExtensions
    {
        public static void LogInformation(this ILogger logger, Guid? correlationId, string message, params object[] args)
        {
            var log = NLog.LogManager.GetCurrentClassLogger();
            var theEvent = new NLog.LogEventInfo(NLog.LogLevel.Info, "", message);
            theEvent.Properties["CorrelationId"] = correlationId.Value;
            theEvent.Properties["Environment"] = StartupExtensions.Environment;
            theEvent.Properties["ApplicationName"] = StartupExtensions.ApplicationName;
            log.Info(theEvent);
        }

        public static void LogWarning(this ILogger logger, Guid? correlationId, string message, params object[] args)
        {
            var log = NLog.LogManager.GetCurrentClassLogger();
            var theEvent = new NLog.LogEventInfo(NLog.LogLevel.Warn, "", message);
            theEvent.Properties["CorrelationId"] = correlationId.Value;
            theEvent.Properties["Environment"] = StartupExtensions.Environment;
            theEvent.Properties["ApplicationName"] = StartupExtensions.ApplicationName;
            log.Warn(theEvent);
        }

        public static void LogError(this ILogger logger, Guid? correlationId, Exception exception, params object[] args)
        {
            var log = NLog.LogManager.GetCurrentClassLogger();
            var theEvent = new NLog.LogEventInfo(NLog.LogLevel.Error, "", exception.Message);
            theEvent.Properties["CorrelationId"] = correlationId.Value;
            theEvent.Properties["Environment"] = StartupExtensions.Environment;
            theEvent.Properties["ApplicationName"] = StartupExtensions.ApplicationName;
            theEvent.Properties["Exception"] = exception;
            log.Error(theEvent);
        }
    }
}
