using FashionHouse.Data.DbModel;
using FashionHouse.Data.ObjectModel;
using FashionHouse.Data.ViewModel;
using System.Collections.Generic;

namespace FashionHouse.Data.IRepository
{
    public interface IProductModelRepository
    {
        Product GetProduct(int sellerId, int productId);

        ProductsView GetProductModels(int sellerId);

        Product PushProductModel(ProductModel productModel, int sellerId);

        ProductCategory PushProductCategory(ProductCategory productCategory, int sellerId);

        void PushProductAttribute(AttributesView attributesView, int _sellerId);

        List<ProductCategory> GetProductCategories();

        List<ProductAttribute> GetProductAttributes();

        List<ProductAttributeValue> GetProductAttributeValues();

        void AssignValues(int id, AttributeValuesView attributeValuesView);

        void DeleteProduct(int id);        
    }
}
