using FashionHouse.Data.DbModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace FashionHouse.Data.ObjectModel
{
    public class ProductModel : Product
    {
        public List<ProductAttribute> ProductAttributes = new List<ProductAttribute>();
        public Seller Seller { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public IFormFile Image { get; set; }
    }
}
