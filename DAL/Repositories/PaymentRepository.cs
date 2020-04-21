using DAL.EF;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DAL.Repositories
{
    public class PaymentRepository : IRepository<Payment>
    {
        private ProductContext db;

        public PaymentRepository(ProductContext context)
        {
            db = context;
        }
        public void AddRange(IEnumerable<Payment> items)
        {
            db.Payments.AddRange(items);
        }

        public void Create(Payment item)
        {
            db.Payments.Add(item);
        }

        public void Delete(int id)
        {
            Payment payment = db.Payments.Find(id);
            if(payment != null)
            {
                db.Payments.Remove(payment);
            }
        }

        public IEnumerable<Payment> Find(Expression<Func<Payment, bool>> predicate)
        {
            return db.Payments.Where(predicate).ToList();
        }

        public Payment Get(int id)
        {
            return db.Payments.Find(id);
        }

        public IEnumerable<Payment> GetAll()
        {
            return db.Payments;
        }

        public void Update(Payment item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified; 
        }
    }
}
