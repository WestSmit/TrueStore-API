using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using DAL.EF;
using System.Linq;

namespace DAL.Repositories
{
    public class ProdsOfMansRepository : IRepository<ProductsOfManufacturer>
    {

        private ProductContext db;

        public ProdsOfMansRepository(ProductContext context)
        {
            db = context;
        }

        public void Create(ProductsOfManufacturer item)
        {
            db.ProductsOfManufacturers.Add(item);
        }

        public void Delete(int id)
        {
            ProductsOfManufacturer item = db.ProductsOfManufacturers.Find(id);
            if (item != null)
            {
                db.ProductsOfManufacturers.Remove(item);
            }
        }

        public IEnumerable<ProductsOfManufacturer> Find(Func<ProductsOfManufacturer, bool> predicate)
        {
            return db.ProductsOfManufacturers.Where(predicate).ToList();
        }

        public ProductsOfManufacturer Get(int id)
        {
            return db.ProductsOfManufacturers.Find(id);
        }

        public IEnumerable<ProductsOfManufacturer> GetAll()
        {
            return db.ProductsOfManufacturers;
        }

        public void Update(ProductsOfManufacturer item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
