using BLL.Models.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class SearchResultModel :BaseModel
    {
        public IEnumerable<SubcategoryModelItem> Subcategories { get; set; }
        public IEnumerable<ProductModelItem> Products { get; set; }
        public IEnumerable<BrandModelItem> Brands { get; set; }

        
    }
}
