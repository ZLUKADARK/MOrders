using Microsoft.AspNetCore.Mvc;
using MOrders.DAL.Entities;
using MOrders.DAL.Interfaces;
using MOrders.Models;
using System.Diagnostics;
using System.Linq;

namespace MOrders.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<Provider> _providerRepository;

        public HomeController(ILogger<HomeController> logger, IRepository<Provider> repository)
        {
            _logger = logger;
            _providerRepository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var providers = await _providerRepository.GetAll();
            ViewBag.Provider = providers.Select(o => o.Name);
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