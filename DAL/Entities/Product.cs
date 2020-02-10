using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Characteristic { get; set; }
        public int Price { get; set; }
        public string Status { get; set; }
        public string Currency { get; set; }
        public bool IsRemoved { get; set; }
        
    }
}
