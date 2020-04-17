using System.Collections.Generic;
using System.Linq;
using BLL.Services.Interfaces;
using DAL.Repositories.Interfaces;
using BLL.Models;
using AutoMapper;
using DAL.Entities;
using BLL.Models.Product;
using System;

namespace BLL.Services
{
    public class ProductServices : IProductService
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;
        public ProductServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _database = unitOfWork;
            _mapper = mapper;
        }

        public ProductModelItem GetProduct(int id)
        {
            return _mapper.Map<ProductModelItem>(_database.ProductRepository.Get(id));
        }

        public IEnumerable<ProductModelItem> GetProductsByCategory(int subcategoryId)
        {
            var Selected = _database.ProductRepository.GetProductsByCategory(subcategoryId);
            return _mapper.Map<IEnumerable<ProductModelItem>>(Selected);
        }

        public void CreateProduct(ProductModelItem model)
        {
            Subcategory subcategory = _database.SubcategoryRepository.Get(model.SubcategoryId);
            if (_database.SubcategoryRepository.Get(model.SubcategoryId) != null)
            {
                Category parentCategory = subcategory.ParentCategory;
                Product product = _mapper.Map<Product>(model);

                _database.ProductRepository.Create(product);
                _database.Save();

                _database.ProductByCategoryRepository.Create(new ProductByCategory { Product = product, Subcategory = subcategory, Category = parentCategory });

                Brand manufacturer;
                var foundManufacturers = _database.BrandRepository.Find(item => item.Name?.ToLower() == model.ManufacturerName.ToLower());
                if (foundManufacturers.Count() == 0)
                {
                    manufacturer = new Brand() { Name = model.ManufacturerName };
                    _database.BrandRepository.Create(manufacturer);
                    _database.Save();
                }
                else
                {
                    manufacturer = foundManufacturers.First();
                }

                _database.ProductByBrandRepository.Create(new ProductByBrand() { Brand = manufacturer, Product = product });
                _database.Save();
            }
            else
            {
                throw new Exception("Category is not found");
            }
        }

        public SearchResultModel SearchProduct(ProductParameters parameters)
        {
            List<Product> items = new List<Product>();
            List<Subcategory> subcategories = new List<Subcategory>();

            if (parameters.SubcategoryId != 0)
            {
                items = _database.ProductRepository.GetProductsByCategory(parameters.SubcategoryId).ToList();
                if (parameters.SearchString != null && parameters.SearchString != "")
                {
                    items = items.Where(item => item.Name.ToUpper().Contains(parameters.SearchString.ToUpper())).ToList();
                    subcategories = _database.SubcategoryRepository.Find(item => item.Name.ToUpper().Contains(parameters.SearchString.ToUpper())).ToList();
                }
            }
            else
            {
                items = _database.ProductRepository.GetProductsByCategory(parameters.SubcategoryId).ToList();
                if (parameters.SearchString != null && parameters.SearchString != "")
                {
                    items = _database.ProductRepository.Find(item => item.Name.ToUpper().Contains(parameters.SearchString.ToUpper())).ToList();
                    subcategories = _database.SubcategoryRepository.Find(item => item.Name.ToUpper().Contains(parameters.SearchString.ToUpper())).ToList();
                }
            }

            SearchResultModel result = new SearchResultModel();
            result.Products = _mapper.Map<IEnumerable<ProductModelItem>>(items);
            result.Subcategories = _mapper.Map<IEnumerable<SubcategoryModelItem>>(subcategories);
            result.Successed = true;
            return result;
        }
    }
}
