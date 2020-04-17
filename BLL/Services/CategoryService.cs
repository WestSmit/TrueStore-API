using System;
using System.Collections.Generic;
using BLL.Services.Interfaces;
using BLL.Models;
using DAL.Repositories.Interfaces;
using DAL.Entities;
using AutoMapper;

namespace BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _database = unitOfWork;
        }

        public void CreateParentCategories(CategoriesModel categoriesModel)
        {
            if (categoriesModel != null)
            {
                _database.CategoryRepository.AddRange(_mapper.Map<IEnumerable<Category>>(categoriesModel.Categories));
                _database.Save();
            }
            else
            {
                throw new Exception("Collection is NULL");
            }            
        }
        public void CreateSubcategories(SubcategoriesModel model)
        {
            List<Subcategory> subcategories = new List<Subcategory>(); 
            for(int i=0; i<model.Subcategories.Count;i++)
            {
                if(_database.CategoryRepository.Get(model.Subcategories[i].ParentCategoryId) == null)
                {
                    throw new Exception("Category is NULL");
                }
                Category parentCategory = _database.CategoryRepository.Get(model.Subcategories[i].ParentCategoryId);
                Subcategory subcategory = _mapper.Map<Subcategory>(model.Subcategories[i]);
                subcategory.ParentCategory = parentCategory;
                subcategories.Add(subcategory);
            }

            _database.SubcategoryRepository.AddRange(subcategories);
            _database.Save();            
        }

        public IEnumerable<CategoryModelItem> GetCategories()
        {
            return _mapper.Map<IEnumerable<CategoryModelItem>>(_database.CategoryRepository.GetAll());
        }

        public IEnumerable<SubcategoryModelItem> GetSubcategories(int id)
        {
            var parentCategory = _database.CategoryRepository.Get(id);
            return _mapper.Map<IEnumerable<SubcategoryModelItem>>(_database.SubcategoryRepository.Find(s=>s.ParentCategory == parentCategory));
        }
    }
}
