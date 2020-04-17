using System.Collections.Generic;
using BLL.Models;
using BLL.Models.Product;

namespace BLL.Services.Interfaces
{
    public interface IProductService
    {
        ProductModelItem GetProduct(int id);
        IEnumerable<ProductModelItem> GetProductsByCategory(int subcategoryId);
        void CreateProduct(ProductModelItem productModelItem);
        SearchResultModel SearchProduct(ProductParameters parameters);
    }
}
