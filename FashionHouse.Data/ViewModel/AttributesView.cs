using FashionHouse.Data.DbModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace FashionHouse.Data.ViewModel
{
    public class AttributesView
    {
        public ProductAttribute ProductAttribute { get; set; }
        public List<ProductAttributeValue> ProductAttributeValues { get; set; }
    }
}
