using System;
using System.Collections.Generic;
using System.Text;
using DAL.EF;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ProductRepository productRepository;
        private BrandRepository  brandRepository;
        private ProductByBrandRepository productByBrandRepository;
        private CategoryRepository categoryRepository;
        private SubcategoryRepository subcategoryRepository;
        private ProductByCategoryRepository productByCategory;

        private OrderRepository orderRepository;
        private OrderItemRepository orderItemRepository;
        private PaymentRepository paymentRepository;

        private UserRepository userRepository;

        private ProductContext db;
        private UserManager<User> userManager;
        

        public EFUnitOfWork(DbContextOptions<ProductContext> options, IServiceProvider serviceProvider)
        {
            db = new ProductContext(options);
            userManager = serviceProvider.GetRequiredService<UserManager<User>>();               
        }

        public  IProductRepository ProductRepository
        {
            get
            {
                if (productRepository == null)
                {
                    productRepository = new ProductRepository(db);
                }
                return productRepository;
            }
        }

        public IRepository<ProductByBrand> ProductByBrandRepository
        {
            get
            {
                if (productByBrandRepository == null)
                {
                    productByBrandRepository = new ProductByBrandRepository(db);
                }
                return productByBrandRepository;
            }
        }

        public IRepository<Brand> BrandRepository
        {
            get
            {
                if (brandRepository == null)
                {
                    brandRepository = new BrandRepository(db);
                }
                return brandRepository;
            }
        }
        public IRepository<Category> CategoryRepository
        {
            get
            {
                if (categoryRepository == null)
                {
                    categoryRepository = new CategoryRepository(db);
                }
                return categoryRepository;
            }
        }
        public IRepository<Subcategory> SubcategoryRepository
        {
            get
            {
                if (subcategoryRepository == null)
                {
                    subcategoryRepository = new SubcategoryRepository(db);
                }
                return subcategoryRepository;
            }
        }

        public IRepository<ProductByCategory> ProductByCategoryRepository
        {
            get
            {
                if(productByCategory == null)
                {
                    productByCategory = new ProductByCategoryRepository(db);
                }
                return productByCategory;
            }
        }

        public IUserRepository UserRepostitory
        {
            get
            {
                if(userRepository == null)
                {
                    userRepository = new UserRepository(userManager);
                }
                return userRepository;
            }
        }

        public IRepository<Order> OrderRepository
        {
            get
            {
                if (orderRepository == null)
                {
                    orderRepository = new OrderRepository(db);
                }
                return orderRepository;
            }
        }

        public IRepository<OrderItem> OrderItemRepository
        {
            get
            {
                if(orderItemRepository == null)
                {
                    orderItemRepository = new OrderItemRepository(db);
                }
                return orderItemRepository;
            }
        }

        public IRepository<Payment> PaymentRepository
        {
            get
            {
                if(paymentRepository == null)
                {
                    paymentRepository = new PaymentRepository(db);
                }
                return paymentRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
