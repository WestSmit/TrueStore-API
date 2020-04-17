using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class SubcategoryModelItem
    {
        public int Id { get; set; }
        public int ParentCategoryId { get; set; }
        public string Name { get; set; }
    }
}
