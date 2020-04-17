using System;
using System.Collections.Generic;
using System.Text;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using DAL.Entities;
using DAL.EF;
using System.Linq;

namespace DAL.Repositories
{
    public class BrandRepository : IRepository<Brand>
    {


        private ProductContext db;

        public BrandRepository(ProductContext context)
        {
            db = context;
        }

        public void AddRange(IEnumerable<Brand> items)
        {
            db.Brands.AddRange(items);
        }

        public void Create(Brand item)
        {
            db.Brands.Add(item);
        }

        public void Delete(int id)
        {
            Brand manufacturer = db.Brands.Find(id);
            if (manufacturer != null)
            {
                db.Brands.Remove(manufacturer);
            }
        }

        public IEnumerable<Brand> Find(Func<Brand, bool> predicate)
        {
            return db.Brands.Where(predicate).ToList();
        }

        public Brand Get(int id)
        {
            return db.Brands.Find(id);
        }

        public IEnumerable<Brand> GetAll()
        {
            return db.Brands;
        }

        public void Update(Brand item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
