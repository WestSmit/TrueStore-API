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
            CreateMap<Product, ProductModelItem>();
            CreateMap<ProductModelItem, Product>();
            CreateMap<Category,  CategoryModelItem>();
            CreateMap<CategoryModelItem,  Category>();
            CreateMap<Subcategory,  SubcategoryModelItem>();
            CreateMap<SubcategoryModelItem,  Subcategory>();
            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>().ForMember(dest => dest.Id, opt => opt.UseDestinationValue());
            CreateMap<OrderItem, OrderItemModel>().ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency.ToString()));
    
        }
    }
}
