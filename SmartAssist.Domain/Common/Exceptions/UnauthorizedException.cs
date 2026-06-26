using System;
using System.Collections.Generic;
using System.Text;

namespace SmartAssist.Domain.Common.Exceptions
{
    public class UnauthorizedException : SmartAssistException
    {
        public UnauthorizedException(string message)
        : base(message)
        {
        }
    }
}
