using FashionHouse.Data.DbModel;
using FashionHouse.Data.IRepository;
using FashionHouse.Data.ObjectModel;
using FashionHouse.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        public ProductModel GetProductModel(int sellerId, int productId)
        {
            ProductModel productModel = new ProductModel();

            return productModel;
        }
        public List<ProductModel> GetProductModels(int sellerId)
        {
            List<ProductModel> productModels = new List<ProductModel>();
            var products = _myContext.Products.Where(x => x.SellerId == sellerId && x.IsDeleted == false).ToList();
            var seller = _myContext.Sellers.Find(sellerId);
            var categories = _myContext.ProductCategories.ToList();
            var attributes = _myContext.ProductAttributes.ToList();

            foreach (var product in products)
            {
                ProductModel productModel = new ProductModel()
                {
                    Count = product.Count,
                    Id = product.Id,
                    IsDeleted = false,
                    MarketingInfo = product.MarketingInfo,
                    Name = product.Name,
                    Price = product.Price,
                    ProductCategories = categories,
                    ProductCategoryId = product.ProductCategoryId,
                    ProductAttributes = attributes,
                    Seller = seller,
                    SellerId = sellerId
                };

                productModels.Add(productModel);
            }

            return productModels;
        }

        public List<ProductCategory> GetProductCategories()
        {
            var productCategories = _myContext.ProductCategories.ToList();
            return productCategories;
        }

        public List<ProductAttribute> GetProductAttributes()
        {
            var productAttributes = _myContext.ProductAttributes.ToList();
            return productAttributes;
        }

        public ProductModel PushProductModel(ProductModel _productModel, int sellerId)  // AddProduct(ProductModel productModel)
        {
            Product product = new Product()
            {
                Name = _productModel.Name,
                Price = _productModel.Price,
                Count = _productModel.Count,
                MarketingInfo = _productModel.MarketingInfo,
                SellerId = sellerId,
                ProductCategoryId = _productModel.ProductCategoryId,
                IsDeleted = false
            };

            _myContext.Products.Add(product);
            _myContext.SaveChanges();

            List<ProductAttribute> productAttributes = _productModel.ProductAttributes;
            List<ProductAttribute> productAttributesFiltered = new List<ProductAttribute>();

            int attributeIdCounter = 0;
            for (int i = 0; i < productAttributes.Count; i++)
            {
                if (productAttributes[i].IsChecked == true)
                {                    
                    var dbAttribute = _myContext.ProductAttributes.Where(x => x.Id == i+1).FirstOrDefault();
                    productAttributesFiltered.Add(dbAttribute);

                    var attributeEntity = new ProductAttributesEntity()
                    {
                        ProductEntityId = product.Id,
                        ProductAttributeEntityId = productAttributesFiltered[attributeIdCounter].Id
                    };
                    attributeIdCounter++;

                    _myContext.ProductAttributesEntities.Add(attributeEntity);
                    _myContext.SaveChanges();
                }
            }            

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

            return null;
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

        public void PushProductAttribute(AttributesView _attributesView, int _sellerId) // AddAttribute(ProductAttribute productAttribute)
        {
            ProductAttribute productAttribute = _attributesView.ProductAttribute;
            List<ProductAttributeValue> productAttributeValues = _attributesView.ProductAttributeValues;

            _myContext.ProductAttributes.Add(productAttribute);
            _myContext.SaveChanges();

            foreach (var attributeValue in productAttributeValues)
            {
                attributeValue.ProductAttributeId = productAttribute.Id;
                _myContext.ProductAttributeValues.Add(attributeValue);
                _myContext.SaveChanges();
            }           
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
