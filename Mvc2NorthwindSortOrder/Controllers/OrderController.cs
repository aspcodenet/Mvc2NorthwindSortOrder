using System;
using System.Linq;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mvc2NorthwindSortOrder.Models;
using Mvc2NorthwindSortOrder.Services;
using Mvc2NorthwindSortOrder.ViewModels;

namespace Mvc2NorthwindSortOrder.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        // GET

        public IActionResult View(int id)
        {
            var orderEnitity = _orderRepository.Get(id);
            var model = new OrderViewModel();
            model.OrderId = orderEnitity.OrderId;
            model.ShipName = orderEnitity.ShipName;
            model.CustomerName = orderEnitity.Customer.CompanyName;

            return View(model);
        }
        //
        //Sortorder asc / desc
        public IActionResult Index(string sortcolumn, string sortorder, string page, string pageSize) //20, 30,50
        {
            var orderListViewModel = new OrderListViewModel();
            orderListViewModel.PagingViewModel.PageSize = string.IsNullOrEmpty(pageSize) ? 
                20 : Convert.ToInt32(pageSize);

            
            var items = _orderRepository.GetAll().Include(order => order.Customer).Select(o=> new OrderListViewModel.OrderViewModel
            {
                CustomerName = o.Customer.CompanyName,
                OrderDate = o.OrderDate,
                OrderId = o.OrderId
            });

            if (string.IsNullOrEmpty(sortorder))
                sortorder = "asc";

            items = AddSorting(items, ref sortcolumn, ref sortorder);

            int currentPage = string.IsNullOrEmpty(page) ? 1 : Convert.ToInt32(page);

            /*
             * OFFSET  (@Page-1)*25 ROWS       -- skip 120 rows
FETCH NEXT 25 ROWS ONLY; -- take 10 rows
             *
             */
            var pageCount = (double)items.Count() / orderListViewModel.PagingViewModel.PageSize;
            orderListViewModel.PagingViewModel.MaxPages = (int)Math.Ceiling(pageCount);

            items = items.Skip((currentPage - 1) * orderListViewModel.PagingViewModel.PageSize).Take(orderListViewModel.PagingViewModel.PageSize);

            //Hur många sidor???               100 / 25
            //int totalNumberOfPages = items.Count()  / PageSize;
            //result.PageCount = (int)Math.Ceiling(pageCount);


            orderListViewModel.PagingViewModel.CurrentPage = currentPage;

            orderListViewModel.Items = items.ToList();
            orderListViewModel.SortColumn = sortcolumn;
            orderListViewModel.SortOrder = sortorder;

            return View(orderListViewModel);
        }

        private IQueryable<OrderListViewModel.OrderViewModel> AddSorting(IQueryable<OrderListViewModel.OrderViewModel> items, ref string sortcolumn, ref string sortorder)
        {
            if (string.IsNullOrEmpty(sortcolumn))
                sortcolumn = "id";
            if (string.IsNullOrEmpty(sortorder))
                sortorder = "asc";


            if (sortcolumn == "datum")
            {
                if (sortorder == "asc")
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

            return items;

        }
    }
}