using FashionHouse.Data.DbModel;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace FashionHouse.Data.ObjectModel
{
    public class ProductModel : Product
    {
        public List<ProductAttribute> ProductAttributes { get; set; }
        public Seller Seller { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public IFormFile Image { get; set; }
    }
}
