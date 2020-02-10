using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Interfaces;
using DAL.EF;
using DAL.Repositories;
using DAL.Interfaces;
using BLL.Models;
using AutoMapper;
using DAL.Entities;

namespace BLL.Services
{
    public class ProductServices : IProductService
    {
        private readonly IUnitOfWork Database;
        private readonly IMapper _mapper;
        public ProductServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.Database = unitOfWork;
            this._mapper = mapper;
        }
        
        public IEnumerable<ProductModel> GetProducts()
        {            
            return _mapper.Map<List<ProductModel>>(Database.ProductsRepository.GetAll());
        }
    }
}
