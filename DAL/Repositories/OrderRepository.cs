using DAL.EF;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public class OrderRepository : IRepository<Order>
    {

        private ProductContext db;

        public OrderRepository(ProductContext context)
        {
            db = context;
        }

        public void AddRange(IEnumerable<Order> items)
        {
            db.Orders.AddRange(items);
        }

        public void Create(Order item)
        {
            db.Add(item);
        }

        public void Delete(int id)
        {
            Order order = db.Orders.Find(id);
            if (order != null)
            {
                db.Orders.Remove(order);
            }
        }

        public IEnumerable<Order> Find(Expression<Func<Order, bool>> predicate)
        {
            return db.Orders.Where(predicate).ToList();
        }

        public Order Get(int id)
        {
            return db.Orders.Find(id);
        }

        public IEnumerable<Order> GetAll()
        {
            return db.Orders;
        }

        public void Update(Order item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
