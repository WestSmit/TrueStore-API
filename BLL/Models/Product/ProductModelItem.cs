using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models.Product
{
    public class ProductModelItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Characteristic { get; set; }
        public int Price { get; set; }
        public string Status { get; set; }
        public string Currency { get; set; }
        public bool IsRemoved { get; set; }
        public string PreviewImg { get; set; }
        public string OtherImgs { get; set; }
        public int SubcategoryId { get; set; }
        public string ManufacturerName { get; set; }
        public int ManufacturerId { get; set; }

    }
}
