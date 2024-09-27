using NLog;
using NLog.Targets;
using System.Collections.Generic;

namespace WinXPCore.Base.Logging
{
    [Target("InvocableMemory")]
    public class InvocableMemoryTarget : TargetWithLayout
    {
        public delegate void MethodCall(IList<string> logs);

        public MethodCall MethodToCall { get; set; }

        public MemoryTarget MemoryTarget { get; set; }

        public InvocableMemoryTarget(string name, string memoryTargetName)
        {
            Name = name;
            MemoryTarget = new MemoryTarget(memoryTargetName);
        }

        protected override void Write(LogEventInfo logEvent)
        {
            DefaultMemorytWrite(logEvent);

            if (MethodToCall != null)
            {
                MethodToCall.Invoke(MemoryTarget.Logs);
            }
        }

        private void DefaultMemorytWrite(LogEventInfo logEvent)
        {
            // https://github.com/NLog/NLog/blob/master/src/NLog/Targets/MemoryTarget.cs

            if (MemoryTarget.MaxLogsCount > 0 && MemoryTarget.Logs.Count >= MemoryTarget.MaxLogsCount)
            {
                MemoryTarget.Logs.RemoveAt(0);
            }

            MemoryTarget.Logs.Add(RenderLogEvent(Layout, logEvent));
        }
    }
}
