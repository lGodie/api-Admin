using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestWork.Infrastructure.Data.Entities
{
    public partial class WorkSubAreas
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdWorkArea { get; set; }
        [JsonIgnore]
        [NotMapped]
        public WorkAreas IdWorkAreaNavigation { get; set; }
    }
}
