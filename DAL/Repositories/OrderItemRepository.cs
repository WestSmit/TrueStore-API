using System;
using System.Collections.Generic;
using System.Text;
using DAL.Repositories.Interfaces;
using DAL.Entities;
using DAL.EF;
using System.Linq;

namespace DAL.Repositories
{
    public class OrderItemRepository : IRepository<OrderItem>
    {
        private ProductContext db;
        public OrderItemRepository(ProductContext context)
        {
            db = context;
        }
        public void AddRange(IEnumerable<OrderItem> items)
        {
            db.OrderItems.AddRange();
        }

        public void Create(OrderItem item)
        {
            db.Add(item);
        }

        public void Delete(int id)
        {
            OrderItem item = db.OrderItems.Find(id);
            if (item != null)
            {
                db.OrderItems.Remove(item);
            }
        }

        public IEnumerable<OrderItem> Find(Func<OrderItem, bool> predicate)
        {
            return db.OrderItems.Where(predicate).ToList();
        }

        public OrderItem Get(int id)
        {
            return db.OrderItems.Find(id);
        }

        public IEnumerable<OrderItem> GetAll()
        {
            return db.OrderItems;
        }

        public void Update(OrderItem item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
