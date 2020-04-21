using System;
using System.Collections.Generic;
using System.Text;
using DAL.Repositories.Interfaces;
using DAL.Entities;
using DAL.EF;
using System.Linq;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private ProductContext db;

        public CategoryRepository(ProductContext context)
        {
            db = context;
        }
        public void Create(Category item)
        {
            db.Add(item);
        }

        public void Delete(int id)
        {
            Category category = db.Categories.Find(id);
            if (category != null)
            {
                db.Categories.Remove(category);
            }
        }

        public IEnumerable<Category> Find(Expression<Func<Category, bool>> predicate)
        {
            return db.Categories.Where(predicate).ToList();
        }

        public Category Get(int id)
        {
            return db.Categories.Find(id);
        }

        public IEnumerable<Category> GetAll()
        {
            return db.Categories;
        }

        public void Update(Category item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void AddRange(IEnumerable<Category> categories)
        {
            db.Categories.AddRange(categories);
        }
    }
}
