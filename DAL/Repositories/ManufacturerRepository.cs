using System;
using System.Collections.Generic;
using System.Text;
using DAL.Repositories;
using DAL.Interfaces;
using DAL.Entities;
using DAL.EF;
using System.Linq;

namespace DAL.Repositories
{
    public class ManufacturerRepository : IRepository<Manufacturer>
    {


        private ProductContext db;

        public ManufacturerRepository(ProductContext context)
        {
            db = context;
        }
        public void Create(Manufacturer item)
        {
            db.Manufacturers.Add(item);
        }

        public void Delete(int id)
        {
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            if (manufacturer != null)
            {
                db.Manufacturers.Remove(manufacturer);
            }
        }

        public IEnumerable<Manufacturer> Find(Func<Manufacturer, bool> predicate)
        {
            return db.Manufacturers.Where(predicate).ToList();
        }

        public Manufacturer Get(int id)
        {
            return db.Manufacturers.Find(id);
        }

        public IEnumerable<Manufacturer> GetAll()
        {
            return db.Manufacturers;
        }

        public void Update(Manufacturer item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
