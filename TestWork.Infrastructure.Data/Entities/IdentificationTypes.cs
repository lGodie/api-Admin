using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestWork.Infrastructure.Data.Entities
{
    public partial class IdentificationTypes
    {
        public IdentificationTypes()
        {
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        [JsonIgnore]
        [NotMapped]
        public ICollection<Users> Users { get; set; }
    }
}
