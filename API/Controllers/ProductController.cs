using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BLL.Models;
using BLL.Services.Interfaces;
using BLL.Models.Product;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productSrvice;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productSrvice = productService;
            _categoryService = categoryService;
        }
        
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            return Ok(_productSrvice.GetProduct(id));
        }

        [HttpGet("{id}")]
        public IEnumerable<ProductModelItem> GetProductsBySubcategory(int id)
        {
            return _productSrvice.GetProductsByCategory(id);
        }
        
        [HttpGet]
        public IEnumerable<CategoryModelItem> GetCategories()
        {
            return _categoryService.GetCategories();
        }

        [HttpGet("{id}")]
        public IEnumerable<SubcategoryModelItem> GetSubcategories(int id)
        {
            return _categoryService.GetSubcategories(id);
        } 
        
        [HttpPost]
        public IActionResult AddProduct([FromBody] ProductModelItem item)
        {
            _productSrvice.CreateProduct(item);
            return Ok();
        }
        [HttpPost]
        public IActionResult AddProducts([FromBody] ProductsModel products)
        {
            foreach (var item in products.Products)
            {
                _productSrvice.CreateProduct(item);
            }
            
            return Ok();
        }

        [HttpPost]
        public IActionResult AddCategories(CategoriesModel categories)
        {            
            _categoryService.CreateParentCategories(categories);
            return Ok();
        }

        [HttpPost]
        public IActionResult AddSubcategories([FromBody] SubcategoriesModel subcategories)
        {            
            _categoryService.CreateSubcategories(subcategories);
            return Ok();
        }

        [HttpGet]
        public IActionResult Search([FromQuery] ProductParameters parameters)
        {
            return Ok(_productSrvice.SearchProduct(parameters));
        }
    }
}
