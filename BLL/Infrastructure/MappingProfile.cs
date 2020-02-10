using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using DAL.Entities;
using BLL.Models;

namespace BLL.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductModel>();
            CreateMap<ProductModel, Product>();
        }
    }
}
