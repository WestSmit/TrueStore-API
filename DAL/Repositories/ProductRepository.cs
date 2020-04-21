using System;
using System.Collections.Generic;
using DAL.Repositories.Interfaces;
using DAL.Entities;
using DAL.EF;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public class ProductRepository : IRepository<Product>, IProductRepository
    {
        private ProductContext db;

        public ProductRepository(ProductContext context)
        {
            db = context;
        }

        public void AddRange(IEnumerable<Product> products)
        {
            db.Products.AddRange(products);
        }

        public void Create(Product item)
        {
            db.Products.Add(item);
        }

        public void Delete(int id)
        {
            Product product = db.Products.Find(id);
            if(product != null)
            {
                db.Products.Remove(product);
            }
        }

        public IEnumerable<Product> Find(Expression<Func<Product, bool>> predicate)
        {
            return db.Products.Where(predicate).ToList();
        }

        public Product Get(int id)
        {
            return db.Products.Find(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return db.Products;
        }

        public IEnumerable<Product> GetProductsByCategory(int subcategoryId)
        {
            return db.ProductsByCategories.Include(e=>e.Product).Include(e=>e.Subcategory)
                .Where(c => c.Subcategory.Id == subcategoryId).Select(p=>p.Product);
        }

        public void Update(Product item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
