using System;
using System.Collections.Generic;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using DAL.EF;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ProductByBrandRepository : IRepository<ProductByBrand>
    {

        private ProductContext db;

        public ProductByBrandRepository(ProductContext context)
        {
            db = context;
        }

        public void AddRange(IEnumerable<ProductByBrand> items)
        {
            db.ProductsByBrands.AddRange(items);
        }

        public void Create(ProductByBrand item)
        {
            db.ProductsByBrands.Add(item);
        }

        public void Delete(int id)
        {
            ProductByBrand item = db.ProductsByBrands.Find(id);
            if (item != null)
            {
                db.ProductsByBrands.Remove(item);
            }
        }

        public IEnumerable<ProductByBrand> Find(Expression<Func<ProductByBrand, bool>> predicate)
        {
            return db.ProductsByBrands
                .Where(predicate)
                .Include(p=>p.Product)
                .Include(p=>p.Brand)
                .ToList();
        }

        public ProductByBrand Get(int id)
        {
            return db.ProductsByBrands.Find(id);
        }

        public IEnumerable<ProductByBrand> GetAll()
        {
            return db.ProductsByBrands;
        }

        public void Update(ProductByBrand item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
