using System;
using System.Collections.Generic;
using System.Text;
using DAL.Interfaces;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductModel> GetProducts();
    }
}
