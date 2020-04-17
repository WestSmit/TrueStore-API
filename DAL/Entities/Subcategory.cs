using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Subcategory
    {
        public int Id { get; set; }
        public Category ParentCategory { get; set; }
        public string Name { get; set; }
    }
}
