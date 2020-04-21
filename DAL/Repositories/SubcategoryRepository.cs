
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
    public class SubcategoryRepository : IRepository<Subcategory>
    {
        private ProductContext db;

        public SubcategoryRepository(ProductContext context)
        {
            db = context;
        }

        public void AddRange(IEnumerable<Subcategory> items)
        {
            db.Subcategories.AddRange(items);
        }

        public void Create(Subcategory item)
        {
            db.Subcategories.Add(item);
        }

        public void Delete(int id)
        {
            Subcategory category = db.Subcategories.Find(id);
            if (category != null)
            {
                db.Subcategories.Remove(category);
            }
        }

        public IEnumerable<Subcategory> Find(Expression<Func<Subcategory, bool>> predicate)
        {
            return db.Subcategories.Where(predicate).ToList();
        }

        public Subcategory Get(int id)
        {
            return db.Subcategories.Where(s => s.Id == id).Include(p => p.ParentCategory).FirstOrDefault();
        }

        public IEnumerable<Subcategory> GetAll()
        {
            return db.Subcategories;
        }

        public void Update(Subcategory item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
