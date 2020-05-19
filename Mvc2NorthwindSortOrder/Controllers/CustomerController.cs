using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Mvc2NorthwindSortOrder.Models;
using Mvc2NorthwindSortOrder.ViewModels;

namespace Mvc2NorthwindSortOrder.Controllers
{
    public class CustomerController : Controller
    {
        private readonly NorthwindContext _context;
        private readonly IMapper _mapper;

        public CustomerController(NorthwindContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET
        public IActionResult Index()
        {


            var model = new CustomerListViewModel();
            foreach (var c in _context.Customers)
            {
                var item = new CustomerListViewModel.CustomerViewModel
                {
                    City = c.City,
                    CompanyName = c.CompanyName,
                    CustomerId = c.CustomerId
                };
                model.Items.Add(item);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.CustomerId == id);
            if (customer == null) return NotFound();

            var model = _mapper.Map<Customers, CustomerEditViewModel>(customer);


            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(CustomerEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = _context.Customers.FirstOrDefault(c => c.CustomerId == model.CustomerId);
                if (customer == null) return NotFound();

                _mapper.Map<CustomerEditViewModel, Customers>(model, customer);
                
                //
                // etc etc etc
                //
                return RedirectToAction("Index");

            }
            return View(model);
        }

    }
}