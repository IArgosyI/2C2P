namespace _2C2P.Common
{
    public interface ILogger
    {
        // Log information message
        void LogInformation(string message);
        
        // Log error message
        void LogError(string message);

        // Log metric and its value
        void LogMetric(string metric, double value);
    }
}