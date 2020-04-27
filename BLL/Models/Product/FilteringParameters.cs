using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models.Product
{
    public class FilteringParameters 
    {
        public int SubcategoryId { get; set; }
        public string SearchString { get; set; }
        public string Brands { get; set; }
        public string Sort { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }


    }
}
