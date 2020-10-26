using System;
using System.Collections.Generic;

namespace TestWork.Infrastructure.Data.Entities
{
    public partial class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Document { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int IdRole { get; set; }
        public int? IdIdentificationType { get; set; }
        public int? IdWorkSubArea { get; set; }
        public bool Active { get; set; }

        public Roles IdIdentificationTypeNavigation { get; set; }
        public Roles IdRoleNavigation { get; set; }
        public WorkAreas IdWorkSubAreaNavigation { get; set; }
    }
}
