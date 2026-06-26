using System;
using System.Collections.Generic;
using System.Text;

namespace SmartAssist.Domain.Common.Exceptions
{
    public class NotFoundException:SmartAssistException
    {
        public NotFoundException(string entityName,object id): base($"Entity \"{entityName}\" ({id}) was not found.")
        {
        }
    }
}
