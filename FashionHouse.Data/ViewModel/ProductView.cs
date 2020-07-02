using FashionHouse.Data.DbModel;
using FashionHouse.Data.ObjectModel;
using System.Collections.Generic;

namespace FashionHouse.Data.ViewModel
{
    public class ProductView
    {
        public ProductModel ProductModel { get; set; }
        public List<AttributesValuesProductEntity> AttributesValuesEntities { get; set; }
        public List<ProductAttributeValue> ProductAttributeValues { get; set; }
        public List<ProductImage> Images { get; set; }
    }
}
