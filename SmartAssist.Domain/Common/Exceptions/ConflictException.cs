using System;
using System.Collections.Generic;
using System.Text;

namespace SmartAssist.Domain.Common.Exceptions
{
    public class ConflictException : SmartAssistException
    {
        public ConflictException(string message)
        : base(message)
        {
        }
    }
}
