using log4net;
using Ecommerce.Core.Interfaces;

namespace Ecommerce.Persistence.Logger
{
    public class Logger : ILogger4Net
    {
        #region  Variables And Constants

        private static readonly ILog log = LogManager.GetLogger(typeof(Logger));

        #endregion

        #region ILogger Members

        public void Log(string message, params object[] args)
        {
            log.Info(string.Format(message, args));
        }

        public void LogError(string message, params object[] args)
        {
            log.Error(string.Format(message, args));
        }

        #endregion
    }

}
