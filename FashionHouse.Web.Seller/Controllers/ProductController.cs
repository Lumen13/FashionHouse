using FashionHouse.Data.DbModel;
using FashionHouse.Data.IRepository;
using FashionHouse.Data.ObjectModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FashionHouse.Web.Seller.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductModelRepository _productModelRepository;
        private readonly int _sellerId = 1;
        public List<ProductCategory> productCategoriesList;

        public ProductController(IProductModelRepository productModelRepository)
        {
            _productModelRepository = productModelRepository;
        }

        [HttpGet, Route("Create")]
        public IActionResult AddProduct()
        {
            productCategoriesList = _productModelRepository.GetProductCategory();
            ProductModel productModel = new ProductModel()
            {
                ProductCategories = productCategoriesList
            };

            return View(productModel);
        }

        [HttpPost, Route("Create")]
        public IActionResult AddProduct(ProductModel productModel)
        {
            _productModelRepository.PushProductModel(productModel, _sellerId);

            return new LocalRedirectResult($"~/Home/Index/");
        }

        [HttpGet, Route("AddCategory")]
        public IActionResult AddCategory()
        {
            return View(new ProductCategory());
        }

        [HttpPost, Route("AddCategory")]
        public IActionResult AddCategory(ProductCategory productCategory)
        {
            _productModelRepository.PushProductCategory(productCategory, _sellerId);

            return new LocalRedirectResult($"~/Home/Index/");
        }
    }
}
