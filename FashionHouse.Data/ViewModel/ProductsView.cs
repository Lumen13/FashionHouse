using FashionHouse.Data.DbModel;
using FashionHouse.Data.ObjectModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace FashionHouse.Data.ViewModel
{
    public class ProductsView
    {
        public List<ProductModel> ProductModels { get; set; }        
        public List<AttributesValuesProductEntity> AttributesValuesEntities { get; set; }
        public List<ProductAttributeValue> ProductAttributeValues { get; set; }
        public List<ProductImage> Images { get; set; }
    }
}
