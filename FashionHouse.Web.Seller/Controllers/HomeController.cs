using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FashionHouse.Data.IRepository;
using FashionHouse.Data.ObjectModel;
using FashionHouse.Web.Seller.Models;

namespace FashionHouse.Web.Seller.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductModelRepository _productModelRepository;

        public HomeController(ILogger<HomeController> logger, IProductModelRepository productModelRepository)
        {
            _productModelRepository = productModelRepository;
            _logger = logger;
        }

        public IActionResult Index(int productId)
        {
            //ProductModel productModel = _productModelRepository.GetProductModel(productId);

            return View(/*productModel*/);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
