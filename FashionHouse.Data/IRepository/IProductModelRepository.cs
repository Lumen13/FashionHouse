using FashionHouse.Data.DbModel;
using FashionHouse.Data.ObjectModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace FashionHouse.Data.IRepository
{
    public interface IProductModelRepository
    {
        List<ProductModel> GetProductModels(int sellerId);

        ProductModel PushProductModel(ProductModel productModel, int sellerId);

        ProductCategory PushProductCategory(ProductCategory productCategory, int sellerId);

        ProductAttribute PushProductAttribute(ProductAttribute productAttribute, int _sellerId);

        void DeleteProduct(int id);
        List<ProductCategory> GetProductCategories();
        List<ProductAttribute> GetProductAttributes();
    }
}
