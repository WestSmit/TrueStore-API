using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class ProductByCategory
    {
        public int Id { get; set; }
        public int ParentCategoryId { get; set; }
        public int ChildCategoryId { get; set; }
        public int ProductId { get; set; }
    }
}
