using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestWork.Infrastructure.Data.Entities
{
    public partial class Roles
    {
        public Roles()
        {
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        [JsonIgnore]
        [NotMapped]
        public ICollection<Users> Users { get; set; }
    }
}
