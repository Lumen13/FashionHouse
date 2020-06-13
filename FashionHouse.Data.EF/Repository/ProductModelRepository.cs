using FashionHouse.Data.DbModel;
using FashionHouse.Data.IRepository;
using FashionHouse.Data.ObjectModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                ProductCategory = productCategory,
                Seller = seller,
                ProductAttributes = productAttributes
            };

            return productModel;
        }

        public ProductModel PushProductModel(ProductModel _productModel, int sellerId)
        {
            Product product = new Product();

            _myContext.SaveChanges();

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
            else
            {

            }

            var productModel = GetProductModel(product.Id);

            return productModel;
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
