using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Mvc2NorthwindSortOrder.Models;
using Mvc2NorthwindSortOrder.ViewModels;

namespace Mvc2NorthwindSortOrder.Controllers
{
    public class SupplierController : Controller
    {
        private readonly NorthwindContext _context;

        public SupplierController(NorthwindContext context)
        {
            _context = context;
        }
        // GET
        public IActionResult Index(string searchWord)
        {
            var model = new SupplierListViewModel();
            var query = _context.Suppliers.Select(r => new SupplierListViewModel.SupplierViewModel
            {
                Id = r.SupplierId,
                City = r.City,
                Company = r.CompanyName,
                ContactName = r.ContactName,
                Country = r.Country
            });
            if (!string.IsNullOrEmpty(searchWord))
            {
                query = query.Where(s => s.Country.Contains(searchWord) || s.Company.Contains(searchWord));
            }
            model.Items = query.ToList();
            return View(model);
        }
    }
}