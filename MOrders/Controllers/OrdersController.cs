using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MOrders.BLL.Interfaces;
using MOrders.BLL.Services;
using MOrders.Domain.DataTransferObject.Order;
using MOrders.Domain.DataTransferObject.OrderItem;
using MOrders.Domain.Models;

namespace Orders.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderServices _orderServices;
        private readonly IOrderItemServices _orderItemServices;
        private readonly IProviderServices _providerServices;

        public OrdersController(
            IOrderServices orderServices, 
            IOrderItemServices orderItemServices,
            IProviderServices providerServices)
        {
            _orderServices = orderServices;
            _orderItemServices = orderItemServices;
            _providerServices = providerServices;
        }

        public async Task<ActionResult> Index()
        {
            var selectValues = await _orderServices.GetDistinct();
            ViewBag.SelectProductsName = new SelectList(selectValues.ItemName, "Name");
            ViewBag.SelectNumber = new SelectList(selectValues.Number, "Number");
            ViewBag.SelectProviderName = new SelectList(selectValues.ProviderName, "ProviderName");
            ViewBag.SelectProductUnit = new SelectList(selectValues.Unit, "Unit");
            ViewBag.DatePast = DateTime.Now.AddMonths(-1);
            ViewBag.DateNow = DateTime.Now;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> OrdersFilter(OrderFilter filter)
        {
            return await GetOrders(filter);
        }

        [HttpGet]
        public async Task<ActionResult> GetOrders(OrderFilter filter)
        {
            var result = await _orderItemServices.GetOrderItemsTable(filter);
            return PartialView(result);
        }

        public async Task<ActionResult> DetailsOrderItem(int id)
        {
            return View(await _orderItemServices.Get(id));
        }

        public async Task<ActionResult> DetailsOrder(int id)
        {
            return View(await _orderServices.GetOrderWithItems(id));
        }

        public async Task<ActionResult> CreateOrder()
        {
            var result = await _providerServices.GetProviders();
            ViewBag.Providers = new SelectList(result, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateOrder([Bind("Number,Date,ProviderId")] OrderDTO order)
        {
            var providers = await _providerServices.GetProviders();
            ViewBag.Providers = new SelectList(providers, "Id", "Name");
            
            if (!ModelState.IsValid)
                return View();

            var result = await _orderServices.Create(order);
            if (result == null)
            {
                ViewBag.ErrorMessage = "Номер заказа уже существует в базе данных. Либо он соотвествует названию заказа, который существует в базе данных"; 
                return View();
            }
                
            try
            {
                return RedirectToAction("CreateOrderItem", new { orderid = result.Id});
            }
            catch
            {
                return View();
            }
        }
        
        public async Task<ActionResult> CreateOrderItem(int? orderid)
        {
            var result = await _orderServices.GetAll();
            if (orderid != null)
                result.Where(a => a.Id == orderid);
            ViewBag.Orders = new SelectList(result, "Id", "Number");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateOrderItem([Bind("Name,Quantity,Unit,OrderId")] OrderItemDTO orderItem, bool next)
        {
            var orders = await _orderServices.GetAll();
            ViewBag.Orders = new SelectList(orders, "Id", "Number");
            
            if (!ModelState.IsValid)
                return View();

            var result = await _orderItemServices.Create(orderItem);
            if (result == null)
            {
                ViewBag.ErrorMessage = "Название заказа соотвествует номеру заказа, который существует в базе данных";
                return View();
            }

            try
            {
                if (next)
                    return RedirectToAction("CreateOrderItem", new { orderid = result.OrderId });
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        
        public async Task<ActionResult> EditOrders(int id)
        {
            var result = await _providerServices.GetProviders();
            ViewBag.Providers = new SelectList(result, "Id", "Name");
            return View(await _orderServices.Get(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditOrders(int id, [Bind("Id,Number,Date,ProviderId")] OrderDTO order)
        {
             if (!ModelState.IsValid)
                return View();

            var result = await _orderServices.Update(id, order);
            if (result == false)
            {
                var providers = await _providerServices.GetProviders();
                ViewBag.Providers = new SelectList(providers, "Id", "Name");
                ViewBag.ErrorMessage = "Номер заказа уже существует в базе данных. Либо он соотвествует названию заказа, который существует в базе данных";
                return View(await _orderServices.Get(id));
            }

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }  
        }

        public async Task<ActionResult> EditOrderItems(int id)
        {
            return View(await _orderItemServices.Get(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditOrderItems(int id, [Bind("Id,Name,Quantity,Unit")] OrderItemDTO item)
        {
            if (!ModelState.IsValid)
                return View(item);
            
            var result = await _orderItemServices.Update(id, item);
            if (result == false)
            {
                ViewBag.ErrorMessage = "Название заказа соотвествует номеру заказа, который существует в базе данных";
                return View(await _orderItemServices.Get(id));
            }

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> DeleteOrderItem(int id)
        {
            var result = await _orderItemServices.Delete(id);
            if (result == true)
                return RedirectToAction("Index");
            return BadRequest();
        }

        public async Task<ActionResult> DeleteOrder(int id)
        {
            var result = await _orderServices.Delete(id);
            if (result == true)
                return RedirectToAction("Index");
            return BadRequest();
        }

    }
}
