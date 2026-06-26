using System;
using System.Collections.Generic;
using System.Text;

namespace SmartAssist.Domain.Common.Exceptions
{
    public class ValidationException : SmartAssistException
    {
        public IEnumerable<string> Errors { get; }

        public ValidationException(IEnumerable<string> errors)
            : base("One or more validation failures occurred.")
        {
            Errors = errors;
        }
    }
}
