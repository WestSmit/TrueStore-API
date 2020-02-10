using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class ChildCategory
    {
        public int Id { get; set; }
        public int ParentCategoryId { get; set; }
        public string Name { get; set; }
    }
}
