using System;
using System.Collections.Generic;
using System.Text;
using TestWork.Common.Enums;

namespace TestWork.Common.Responses
{
    public class UserResponse
    {
        public string Document { get; set; }
        public string Name { get; set; }
        public string IdentificationTypes { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string WorkSubArea { get; set; }
        public string Role { get; set; }
    }
}
