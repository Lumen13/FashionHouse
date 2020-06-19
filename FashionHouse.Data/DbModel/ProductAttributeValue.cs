using System;
using System.Collections.Generic;
using System.Text;

namespace FashionHouse.Data.DbModel
{
    public class ProductAttributeValue
    {
        public int Id { get; set; }
        public int ProductAttributeId { get; set; }
        public string AttributeValue { get; set; }
    }
}
