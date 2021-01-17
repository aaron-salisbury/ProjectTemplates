using System;

namespace WinXPCore.Base
{
    public class LoggableObject
    {
        public static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
    }
}
