using System;
using System.Collections.Generic;
using System.Text;

namespace FashionHouse.Data.DbModel
{
    public class ProductAttributesEntity
    {
        public int Id { get; set; }
        public int ProductEntityId { get; set; }
        public int ProductAttributeEntityId { get; set; }
    }
}