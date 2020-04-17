using System;
using System.Collections.Generic;
using DAL.Repositories.Interfaces;
using DAL.Entities;
using DAL.EF;
using System.Linq;

namespace DAL.Repositories
{
    public class ProductByCategoryRepository : IRepository<ProductByCategory>
    {
        private ProductContext db;

        public ProductByCategoryRepository(ProductContext context)
        {
            db = context;
        }

        public void AddRange(IEnumerable<ProductByCategory> items)
        {
            db.ProductsByCategories.AddRange(items);
        }

        public void Create(ProductByCategory item)
        {
            db.ProductsByCategories.Add(item);
        }

        public void Delete(int id)
        {
            ProductByCategory item = db.ProductsByCategories.Find(id);
            if(item != null)
            {
                db.ProductsByCategories.Remove(item);
            }
        }

        public IEnumerable<ProductByCategory> Find(Func<ProductByCategory, bool> predicate)
        {
            return db.ProductsByCategories.Where(predicate);
        }

        public ProductByCategory Get(int id)
        {
            return db.ProductsByCategories.Find(id);
        }

        public IEnumerable<ProductByCategory> GetAll()
        {
            return db.ProductsByCategories;
        }

        public void Update(ProductByCategory item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
