using FashionHouse.Data.DbModel;
using System.Collections.Generic;

namespace FashionHouse.Data.ViewModel
{
    public class AttributeValuesView
    {
        public List<ProductAttributeValue> ProductAttributeValues { get; set; }
        public List<ProductAttribute> ProductAttributes { get; set; }
        public Product Product { get; set; }
    }
}
