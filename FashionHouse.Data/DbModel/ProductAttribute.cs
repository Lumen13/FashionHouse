using System;
using System.Collections.Generic;
using System.Text;

namespace FashionHouse.Data.DbModel
{
    public class ProductAttribute
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public string Description { get; set; }
        public bool IsChecked { get; set; }
    }
}
