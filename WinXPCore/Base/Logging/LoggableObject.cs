using System;

namespace WinXPCore.Base.Logging
{
    public class LoggableObject
    {
        public static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
    }
}
