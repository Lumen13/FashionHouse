using FashionHouse.Data.DbModel;
using FashionHouse.Data.ObjectModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace FashionHouse.Data.ViewModel
{
    public class AttributeValuesView
    {
        public List<ProductAttributeValue> ProductAttributeValues { get; set; }
        public List<ProductAttribute> ProductAttributes { get; set; }
        public Product Product { get; set; }
    }
}
