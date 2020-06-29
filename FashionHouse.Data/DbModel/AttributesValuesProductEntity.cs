using System;
using System.Collections.Generic;
using System.Text;

namespace FashionHouse.Data.DbModel
{
    public class AttributesValuesProductEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ProductAttributeId { get; set; }
        public int ProductAttributeValueId { get; set; }
        public int Pieces { get; set; }
    }
}
