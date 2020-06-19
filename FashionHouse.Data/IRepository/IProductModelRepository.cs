using FashionHouse.Data.DbModel;
using FashionHouse.Data.ObjectModel;
using FashionHouse.Data.ViewModel;
using System.Collections.Generic;

namespace FashionHouse.Data.IRepository
{
    public interface IProductModelRepository
    {
        ProductModel GetProductModel(int sellerId, int productId);

        List<ProductModel> GetProductModels(int sellerId);

        ProductModel PushProductModel(ProductModel productModel, int sellerId);

        ProductCategory PushProductCategory(ProductCategory productCategory, int sellerId);

        void PushProductAttribute(AttributesView attributesView, int _sellerId);

        List<ProductCategory> GetProductCategories();

        List<ProductAttribute> GetProductAttributes();

        void DeleteProduct(int id);        
    }
}
