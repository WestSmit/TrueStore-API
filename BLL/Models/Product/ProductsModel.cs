using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models.Product
{
    public class ProductsModel :BaseModel
    {
        public IEnumerable<ProductModelItem> Products { get; set; }

    }
}
