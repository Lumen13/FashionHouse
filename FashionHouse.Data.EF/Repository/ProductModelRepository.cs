﻿using FashionHouse.Data.DbModel;
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
        public Product GetProduct(int sellerId, int productId)
        {
            Product product = _myContext.Products.Where(x => x.Id == productId).FirstOrDefault();

            return product;
        }
        public ProductView GetProductModel(int sellerId, int productId)
        {
            var product = _myContext.Products.Where(x => x.SellerId == sellerId &&
                                                         x.IsDeleted == false &&
                                                         x.Id == productId).FirstOrDefault();
            var seller = _myContext.Sellers.Find(sellerId);
            var categories = _myContext.ProductCategories.ToList();
            var attributes = _myContext.ProductAttributes.ToList();
            var images = _myContext.ProductImages.ToList();

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

            var attributesValuesEntities = _myContext.AttributesValuesProductEntities.ToList();
            var attributesValues = _myContext.ProductAttributeValues.ToList();

            ProductView productView = new ProductView()
            {
                AttributesValuesEntities = attributesValuesEntities,
                ProductAttributeValues = attributesValues,
                Images = images,
                ProductModel = productModel
            };

            return productView;
        }
            
    public ProductsView GetProductModels(int sellerId)
    {
        List<ProductModel> productModels = new List<ProductModel>();
        var products = _myContext.Products.Where(x => x.SellerId == sellerId && x.IsDeleted == false).ToList();
        var seller = _myContext.Sellers.Find(sellerId);
        var categories = _myContext.ProductCategories.ToList();
        var attributes = _myContext.ProductAttributes.ToList();
        var images = _myContext.ProductImages.ToList();

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

        var attributesValuesEntities = _myContext.AttributesValuesProductEntities.ToList();
        var attributesValues = _myContext.ProductAttributeValues.ToList();

        ProductsView productsView = new ProductsView()
        {
            ProductModels = productModels,
            AttributesValuesEntities = attributesValuesEntities,
            ProductAttributeValues = attributesValues,
            Images = images
        };

        return productsView;
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

    public List<ProductAttributeValue> GetProductAttributeValues()
    {
        var productAttributeValues = _myContext.ProductAttributeValues.ToList();
        return productAttributeValues;
    }

    public Product PushProductModel(ProductModel _productModel, int sellerId)  // AddProduct(ProductModel productModel)
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

        if (_productModel.Images != null)                                                                   // saving image
        {
            for (int i = 0; i < _productModel.Images.Count; i++)
            {
                var fullPath = $"{_webRootPath}\\wwwroot\\Images\\sellerId_{sellerId}\\productId_{product.Id}\\";
                var imageName = i + 1 + "." + _productModel.Images[i].ContentType.Split("/").Last();

                var ImageURL = fullPath + imageName;
                ProductImage dbImage = new ProductImage()
                {
                    ImagePath = ImageURL,
                    ProductId = product.Id
                };

                if (Directory.Exists(fullPath) == false)
                {
                    Directory.CreateDirectory(fullPath);
                }

                using var fileStream = new FileStream(fullPath + imageName, FileMode.Create);
                _productModel.Images[i].CopyTo(fileStream);

                _myContext.ProductImages.Add(dbImage);
            }
        }

        List<ProductAttribute> productAttributes = _productModel.ProductAttributes;
        List<ProductAttribute> productAttributesFiltered = new List<ProductAttribute>();

        int attributeIdCounter = 0;
        for (int i = 0; i < productAttributes.Count; i++)
        {
            if (productAttributes[i].IsChecked == true)
            {
                var dbAttribute = _myContext.ProductAttributes.Where(x => x.Id == i + 1).FirstOrDefault();   // 1 - is not const!
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

        return product;
    }

    public ProductCategory PushProductCategory(ProductCategory _productCategory, int _sellerId) // AddCategory(ProductCategory productCategory)
    {
        ProductCategory parentCategory = new ProductCategory();

        if (_productCategory.ParentId != null)
        {
            parentCategory = _myContext.ProductCategories.Where(x => x.Id == _productCategory.ParentId).FirstOrDefault();
            parentCategory.IsParent = true;
        }

        ProductCategory productCategory = new ProductCategory()
        {
            Id = _productCategory.Id,
            ParentId = _productCategory.ParentId,
            IsParent = _productCategory.IsParent,
            Name = _productCategory.Name,
            ParentName = parentCategory.Name,
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

    public void AssignValues(int id, AttributeValuesView attributeValuesView)
    {
        AttributesValuesProductEntity productAttributeValuesProducts = new AttributesValuesProductEntity();
        ProductAttributeValue dbValue;

        for (int i = 0; i < attributeValuesView.ProductAttributeValues.Count; i++)
        {
            if (attributeValuesView.ProductAttributeValues[i].AttributeValue != null &&
                attributeValuesView.ProductAttributeValues[i].AttributeValue != "0")
            {
                switch (i)
                {
                    case 16:                                                                                // not const
                        attributeValuesView.ProductAttributeValues[i].ProductAttributeId = 3;               // not const
                        productAttributeValuesProducts.Pieces = 1;
                        dbValue = _myContext.ProductAttributeValues.Where(x =>
                            x.AttributeValue == attributeValuesView.ProductAttributeValues[i].AttributeValue).FirstOrDefault();
                        break;
                    case 17:
                        attributeValuesView.ProductAttributeValues[i].ProductAttributeId = 4;
                        productAttributeValuesProducts.Pieces = 1;
                        dbValue = _myContext.ProductAttributeValues.Where(x =>
                            x.AttributeValue == attributeValuesView.ProductAttributeValues[i].AttributeValue).FirstOrDefault();
                        break;
                    case 18:
                        attributeValuesView.ProductAttributeValues[i].ProductAttributeId = 5;
                        productAttributeValuesProducts.Pieces = 1;
                        dbValue = _myContext.ProductAttributeValues.Where(x =>
                            x.AttributeValue == attributeValuesView.ProductAttributeValues[i].AttributeValue).FirstOrDefault();
                        break;
                    default:
                        dbValue = _myContext.ProductAttributeValues.Where(x => x.Id == i + 1).FirstOrDefault();
                        productAttributeValuesProducts.Pieces = Convert.ToInt32(attributeValuesView.ProductAttributeValues[i].AttributeValue);
                        if (i < 9)
                            attributeValuesView.ProductAttributeValues[i].ProductAttributeId = 1;           // size part 1                                                                                                                
                        else
                            attributeValuesView.ProductAttributeValues[i].ProductAttributeId = 2;           // size part 2
                        break;
                }

                productAttributeValuesProducts.Id = 0;
                productAttributeValuesProducts.ProductId = id;
                productAttributeValuesProducts.ProductAttributeId = attributeValuesView.ProductAttributeValues[i].ProductAttributeId;
                productAttributeValuesProducts.ProductAttributeValueId = dbValue.Id;

                _myContext.AttributesValuesProductEntities.Add(productAttributeValuesProducts);
                _myContext.SaveChanges();
            }
        }
    }
}
}
