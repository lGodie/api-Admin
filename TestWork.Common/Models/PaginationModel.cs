using System;
using System.Collections.Generic;
using System.Text;

namespace TestWork.Common.Models
{
    public class PaginationModel
    {
        public int PageQuantity { get; set; }
        public int ActualPage { get; set; }
        public int PageSize { get; set; }
        public string searchString { get; set; }
        public int idrole { get; set; }

    }
}
