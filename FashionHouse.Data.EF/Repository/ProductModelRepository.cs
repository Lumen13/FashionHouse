using FashionHouse.Data.DbModel;
using FashionHouse.Data.IRepository;
using FashionHouse.Data.ObjectModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace FashionHouse.Data.EF.Repository
{
    public class ProductModelRepository : IProductModelRepository
    {
        private MyContext _myContext { get; set; }
        private string _webRootPath { get; set; }

        public ProductModelRepository(string connectionString, string webRootPath)
        {
            _myContext = new MyContext(connectionString);
            _webRootPath = webRootPath;
        }

        public ProductModel GetProductModel(int productId)
        {
            var product = _myContext.Products.Find(productId);

            if (product == null || product.IsDeleted == true)
            {
                return null;
            }

            var seller = _myContext.Sellers.Find(product.SellerId);
            var productAttributes = _myContext.ProductAttributes.Where(x => x.ProductId == productId).ToList();
            var productCategory = _myContext.ProductCategories.Find(product.ProductCategoryId);

            ProductModel productModel = new ProductModel()
            {
                Id = product.Id,
                ProductCategoryId = productCategory.Id,
                SellerId = seller.Id,
                Name = product.Name,
                Count = product.Count,
                Price = product.Price,
                MarketingInfo = product.MarketingInfo,
                //ProductCategories = productCategory,
                Seller = seller,
                ProductAttributes = productAttributes
            };

            return productModel;
        }

        public List<ProductCategory> GetProductCategory()
        {
            var productCategory = _myContext.ProductCategories.ToList();
            return productCategory;
        }

        public ProductModel PushProductModel(ProductModel _productModel, int sellerId)  // AddProduct(ProductModel productModel)
        {

            //ProductModel productModel = new ProductModel();
            //productModel.Id = _productModel.Id;
            //productModel.Name = _productModel.Name;
            //productModel.Price = _productModel.Price;
            //productModel.Count = _productModel.Count;
            //productModel.MarketingInfo = _productModel.MarketingInfo;
            //productModel.Seller = _productModel.Seller;
            //productModel.SellerId = _productModel.SellerId;
            //productModel.ProductCategory = _productModel.ProductCategory;
            //productModel.ProductCategoryId = _productModel.ProductCategoryId;
            //productModel.IsDeleted = false;

            Product product = new Product()
            {
                Id = _productModel.Id,
                Name = _productModel.Name,
                Price = _productModel.Price,
                Count = _productModel.Count,
                MarketingInfo = _productModel.MarketingInfo,
                SellerId = sellerId,
                ProductCategoryId = _productModel.ProductCategoryId,
                IsDeleted = false
            };        

            if (_productModel.Image != null)
            {
                var imagePath = $"Images\\sellerId_{sellerId}\\productId_{product.Id}\\";

                var fullPath = $"{_webRootPath}\\wwwroot\\{imagePath}";

                var imageName = Guid.NewGuid() + "." + _productModel.Image.ContentType.Split("/").Last();

                var ImageURL = imagePath + imageName;

                if (Directory.Exists(fullPath) == false)
                {
                    Directory.CreateDirectory(fullPath);
                }

                using (var fileStream = new FileStream(fullPath + imageName, FileMode.Create))
                {
                    _productModel.Image.CopyTo(fileStream);
                }

                _myContext.Products.Add(product);
                _myContext.SaveChanges();
            }            

            _myContext.Products.Add(product);
            _myContext.SaveChanges();

            var productModel = GetProductModel(product.Id);

            return productModel;
        }

        public ProductCategory PushProductCategory(ProductCategory _productCategory, int _sellerId) // AddCategory(ProductCategory productCategory)
        {
            ProductCategory productCategory = new ProductCategory()
            {
                Id = _productCategory.Id,
                ParentId = _productCategory.ParentId,
                Name = _productCategory.Name,
                Description = _productCategory.Description
            };

            _myContext.ProductCategories.Add(productCategory);
            _myContext.SaveChanges();

            return productCategory;
        }

        public void DeleteProduct(int id)
        {
            var products = _myContext.Products.Where(x => x.Id == id);
            foreach (var product in products)
            {
                product.IsDeleted = true;
            }

            _myContext.SaveChanges();
        }        
    }
}
