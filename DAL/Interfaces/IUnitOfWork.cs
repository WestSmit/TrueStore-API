using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUnitOfWork 
    {
        IRepository<Product> ProductsRepository { get;}
        IRepository<ProductsOfManufacturer> ProdsOfMansRepository { get; }
        IRepository<Manufacturer> ManufacturersRepository { get; }
        void Save();

    }
}
