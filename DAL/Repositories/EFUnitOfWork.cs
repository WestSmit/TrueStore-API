using System;
using System.Collections.Generic;
using System.Text;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ProductsRepository productsRepository;
        private ManufacturerRepository  manufacturerRepository;
        private ProdsOfMansRepository prodsOfMansRepository;

        private ProductContext db;

        public EFUnitOfWork(DbContextOptions<ProductContext> options)
        {
            db = new ProductContext(options);
        }

        public  IRepository<Product> ProductsRepository
        {
            get
            {
                if(productsRepository == null)
                {
                    productsRepository = new ProductsRepository(db);
                };
                return productsRepository;
            }
        }

        public IRepository<ProductsOfManufacturer> ProdsOfMansRepository
        {
            get
            {
                if (prodsOfMansRepository == null)
                {
                    prodsOfMansRepository = new ProdsOfMansRepository(db);
                };
                return prodsOfMansRepository;

            }
        }

        public IRepository<Manufacturer> ManufacturersRepository
        {
            get
            {
                if (manufacturerRepository == null)
                {
                    manufacturerRepository = new ManufacturerRepository(db);
                };
                return manufacturerRepository;

            }
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
