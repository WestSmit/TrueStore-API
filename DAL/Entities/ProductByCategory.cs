using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class ProductByCategory
    {
        public int Id { get; set; }        
        public Category Category { get; set; }
        public Subcategory Subcategory { get; set; }
        public Product Product { get; set; }
    }
}
