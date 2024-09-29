using System;

namespace DotNetFramework.Core.Logging
{
    internal class FormattedLogValues
    {
        public string OriginalMessage { get; set; }
        public object[] MessageArguments { get; set; }

        internal FormattedLogValues(string message, params object[] args)
        {
            OriginalMessage = message;
            MessageArguments = args;
        }

        public override string ToString()
        {
            string formattedMessage = string.Empty;

            if (OriginalMessage != null)
            {
                if (MessageArguments != null && MessageArguments.Length > 0)
                {
                    formattedMessage = string.Format(OriginalMessage, MessageArguments);
                }
                else
                {
                    formattedMessage = OriginalMessage;
                }
            }

            return formattedMessage;
        }
    }
}
