using FashionHouse.Data.DbModel;
using FashionHouse.Data.IRepository;
using FashionHouse.Data.ObjectModel;
using FashionHouse.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FashionHouse.Web.Seller.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductModelRepository _productModelRepository;
        private readonly int _sellerId = 1;
        public List<ProductCategory> productCategoriesList;
        public List<ProductAttribute> productAttributeList;

        public ProductController(IProductModelRepository productModelRepository)
        {
            _productModelRepository = productModelRepository;
        }

        [HttpGet, Route("Create")]
        public IActionResult AddProduct()
        {
            productCategoriesList = _productModelRepository.GetProductCategories();
            productAttributeList = _productModelRepository.GetProductAttributes();

            ProductModel productModel = new ProductModel()
            {
                ProductAttributes = productAttributeList,
                ProductCategories = productCategoriesList
            };

            return View(productModel);
        }

        [HttpPost, Route("Create")]
        public IActionResult AddProduct(ProductModel productModel)
        {
            var productAfterPush = _productModelRepository.PushProductModel(productModel, _sellerId);

            return Redirect($"AddAttributeValues/{productAfterPush.Id}");
        }

        [HttpGet, Route("AddCategory")]
        public IActionResult AddCategory()
        {
            var categories = _productModelRepository.GetProductCategories();

            return View(categories);
        }

        [HttpPost, Route("AddCategory")]
        public IActionResult AddCategory(ProductCategory productCategory)
        {
            _productModelRepository.PushProductCategory(productCategory, _sellerId);

            return new LocalRedirectResult($"~/Create/");
        }

        [HttpGet, Route("AddAttribute")]
        public IActionResult AddAttribute()
        {
            return View(new AttributesView());
        }

        [HttpPost, Route("AddAttribute")]
        public IActionResult AddAttribute(AttributesView attributesView)
        {
            _productModelRepository.PushProductAttribute(attributesView, _sellerId);

            return new LocalRedirectResult($"~/Home/Index/");
        }

        [Route("Products")]
        public IActionResult Products()
        {
            ProductsView productsView = _productModelRepository.GetProductModels(_sellerId);

            return View(productsView);
        }

        [Route("Product/{id}")]
        public IActionResult Product(int id)
        {
            ProductView productView = _productModelRepository.GetProductModel(_sellerId, id);

            return View(productView);
        }

        [HttpGet, Route("AddAttributeValues/{id}")]
        public IActionResult AddAttributeValues(int id)
        {
            var pAttributes = _productModelRepository.GetProductAttributes();
            var pAttributeValues = _productModelRepository.GetProductAttributeValues();
            var product = _productModelRepository.GetProduct(_sellerId, id);

            AttributeValuesView attributeValuesView = new AttributeValuesView()
            {
                ProductAttributes = pAttributes,
                ProductAttributeValues = pAttributeValues,
                Product = product
            };

            return View(attributeValuesView);
        }

        [HttpPost, Route("AddAttributeValues/{id}")]
        public IActionResult AddAttributeValues(AttributeValuesView attributeValuesView, int id)
        {
            _productModelRepository.AssignValues(id, attributeValuesView);

            return new LocalRedirectResult($"~/Home/Index/");
        }
    }
}
