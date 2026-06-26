using System;
using System.Collections.Generic;
using System.Text;

namespace SmartAssist.Domain.Common.Exceptions
{
    public abstract class SmartAssistException : Exception
    {
        protected SmartAssistException(string message) : base(message)
        {
        }
        protected SmartAssistException(string message, Exception innerException)
        : base(message, innerException)
        {
        }
    }
}
