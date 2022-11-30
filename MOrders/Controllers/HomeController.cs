using Microsoft.AspNetCore.Mvc;
using MOrders.BLL.Interfaces;
using MOrders.Models;
using System.Diagnostics;
using System.Linq;

namespace MOrders.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrderServices _orderServices;
        public HomeController(ILogger<HomeController> logger, IOrderServices orderServices)
        {
            _logger = logger;
            _orderServices = orderServices;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Provider = await _orderServices.GetProviders();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}