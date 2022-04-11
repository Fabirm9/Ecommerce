namespace Ecommerce.Core.Interfaces
{
    public interface ILogger4Net
    {
        void Log(string message, params object[] args);
        void LogError(string message, params object[] args);
    }
}
