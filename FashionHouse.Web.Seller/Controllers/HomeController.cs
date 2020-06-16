using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FashionHouse.Data.IRepository;
using FashionHouse.Data.ObjectModel;
using FashionHouse.Web.Seller.Models;
using System.Collections.Generic;

namespace FashionHouse.Web.Seller.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductModelRepository _productModelRepository;
        private readonly int _sellerId = 1;

        public HomeController(ILogger<HomeController> logger, IProductModelRepository productModelRepository)
        {
            _productModelRepository = productModelRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<ProductModel> productModels = _productModelRepository.GetProductModels(_sellerId);

            return View(productModels);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
