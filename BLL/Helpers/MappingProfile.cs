using AutoMapper;
using DAL.Entities;
using BLL.Models;
using BLL.Models.Order;
using BLL.Models.Product;

namespace BLL.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductModelItem>().ReverseMap();
            CreateMap<Category,  CategoryModelItem>().ReverseMap();
            CreateMap<Subcategory,  SubcategoryModelItem>().ReverseMap();
            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>().ForMember(dest => dest.Id, opt => opt.UseDestinationValue());
            CreateMap<OrderItem, OrderItemModel>().ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency.ToString()));
            CreateMap<Brand, BrandModelItem>();
    
        }
    }
}
