using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mvc2NorthwindSortOrder.Models;
using Mvc2NorthwindSortOrder.ViewModels;

namespace Mvc2NorthwindSortOrder.Controllers
{
    public class OrderController : Controller
    {
        private readonly NorthwindContext _context;

        public OrderController(NorthwindContext context)
        {
            _context = context;
        }
        // GET
        //
        //Sortorder asc / desc
        public IActionResult Index(string sortcolumn, string sortorder)
        {
            var orderListViewModel = new OrderListViewModel();

            var items = _context.Orders.Include(order => order.Customer).Select(o=> new OrderListViewModel.OrderViewModel
            {
                CustomerName = o.Customer.CompanyName,
                OrderDate = o.OrderDate,
                OrderId = o.OrderId
            });

            if (string.IsNullOrEmpty(sortorder))
                sortorder = "asc";


            if (sortcolumn == "datum")
            {
                if(sortorder == "asc")
                    items = items.OrderBy(p => p.OrderDate);
                else
                    items = items.OrderByDescending(p => p.OrderDate);
            }
            else if (sortcolumn == "id")
            {
                if (sortorder == "asc")
                    items = items.OrderBy(p => p.OrderId);
                else
                    items = items.OrderByDescending(p => p.OrderId);

            }
            else 
            {
                if (sortorder == "asc")
                    items = items.OrderBy(p => p.CustomerName);
                else
                    items = items.OrderByDescending(p => p.CustomerName);

                sortcolumn = "namn";
            }


            orderListViewModel.Items = items.ToList();
            orderListViewModel.SortColumn = sortcolumn;
            orderListViewModel.SortOrder = sortorder;

            return View(orderListViewModel);
        }
    }
}