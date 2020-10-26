using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestWork.Infrastructure.Data.Entities
{
    public partial class WorkAreas
    {
        public WorkAreas()
        {
            Users = new HashSet<Users>();
            WorkSubAreas = new HashSet<WorkSubAreas>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        [NotMapped]
        public ICollection<Users> Users { get; set; }
        [JsonIgnore]
        [NotMapped]
        public ICollection<WorkSubAreas> WorkSubAreas { get; set; }
    }
}
