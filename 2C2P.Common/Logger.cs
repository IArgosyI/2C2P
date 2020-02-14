using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2C2P.Common
{
    public class Logger : ILogger
    {
        private readonly TelemetryClient _telemetryClient;
        private readonly IDictionary<string, string> _commonProperties;

        public Logger()
        {
            _telemetryClient = new TelemetryClient();
            _commonProperties = GenerateCommonProperties();
        }

        public void LogInformation(string message)
        {
            _telemetryClient.TrackTrace(message, SeverityLevel.Information, _commonProperties);
        }

        public void LogError(string message)
        {
            _telemetryClient.TrackTrace(message, SeverityLevel.Error, _commonProperties);
        }

        public void LogMetric(string metric, double value)
        {
            _telemetryClient.GetMetric(metric).TrackValue(value);
        }

        private IDictionary<string, string> GenerateCommonProperties()
        {
            return new Dictionary<string, string>()
                {
                    {"Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") }
                };

        }
    }
}
