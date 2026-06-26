using System;
using System.Collections.Generic;
using System.Text;

namespace SmartAssist.Domain.Common.Exceptions
{
    public class BusinessRuleException : SmartAssistException
    {
        public BusinessRuleException(string message)
        : base(message)
        {
        }
    }
}
