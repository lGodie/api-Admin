using System;
using System.Collections.Generic;
using System.Text;

namespace TestWork.Common.Requests
{
    public class UserRequest
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public int IdWorkSubArea { get; set; }
        public int IdIdentificationTypes { get; set; }
        public int IdRole{ get; set; }
    }
}
