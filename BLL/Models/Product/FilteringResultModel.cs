using BLL.Models.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models.Product
{
    public class FilteringResultModel : BaseModel
    {
        public IEnumerable<SubcategoryModelItem> Subcategories { get; set; }
        public IEnumerable<ProductModelItem> Products { get; set; }
        public IEnumerable<BrandModelItem> Brands { get; set; }
        public int[] SelectedBrands { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public int SetMinPrice { get; set; }
        public int SetMaxPrice { get; set; }


    }
}
