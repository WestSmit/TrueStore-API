using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class ProductsOfManufacturer
    {
        public int Id { get; set; }
        public int ManufacturerId { get; set; }
        public int ProductId { get; set; }
        public DateTime Date { get; set; }
    }
}
