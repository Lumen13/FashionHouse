using FashionHouse.Data.ObjectModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace FashionHouse.Data.IRepository
{
    public interface IProductModelRepository
    {
        ProductModel GetProductModel(int id);

        ProductModel PushProductModel(ProductModel productModel, int sellerId);

        void DeleteProduct(int id);
    }
}
