using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using DAL.Repositories;

namespace DAL.Repositories.Interfaces
{
    public interface IUnitOfWork 
    {
        IProductRepository ProductRepository { get;}
        IRepository<ProductByBrand> ProductByBrandRepository { get; }
        IRepository<Brand> BrandRepository { get; }
        IRepository<Category> CategoryRepository { get; }
        IRepository<Subcategory> SubcategoryRepository { get; }
        IRepository<ProductByCategory> ProductByCategoryRepository { get; }
        
        IRepository<Order> OrderRepository { get; }
        IRepository<OrderItem> OrderItemRepository { get; }
        IRepository<Payment> PaymentRepository { get; }
        
        IUserRepository UserRepostitory { get; }

        void Save();

    }
}
