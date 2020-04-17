using System.Collections.Generic;
using BLL.Models;

namespace BLL.Services.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<CategoryModelItem> GetCategories();
        IEnumerable<SubcategoryModelItem> GetSubcategories(int ParentCategoryId);
        void CreateParentCategories(CategoriesModel model);
        void CreateSubcategories(SubcategoriesModel model);

    }
}
