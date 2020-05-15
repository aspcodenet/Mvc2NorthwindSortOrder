using System;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mvc2NorthwindSortOrder.Models;
using Mvc2NorthwindSortOrder.ViewModels;

namespace Mvc2NorthwindSortOrder.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly NorthwindContext _context;

        public EmployeesController(NorthwindContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            return View();
        }



        public IActionResult Edit(int id)
        {
            return View(id);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit()
        {
            return RedirectToAction("Index");
        }


        public IActionResult GetRows()
        {
            var filter = GetFilter();
            var q = _context.Employees as IQueryable<Employees>;
            if (!string.IsNullOrEmpty(filter.FirstName))
                q = q.Where(r => r.FirstName.Contains(filter.FirstName));
            if (!string.IsNullOrEmpty(filter.LastName))
                q = q.Where(r => r.LastName.Contains(filter.LastName));
            if (!string.IsNullOrEmpty(filter.Title))
                q = q.Where(r => r.Title.Contains(filter.Title));
            if (!string.IsNullOrEmpty(filter.Homephone))
                q = q.Where(r => r.HomePhone.Contains(filter.Homephone));
            if (!string.IsNullOrEmpty(filter.City))
                q = q.Where(r => r.City.Contains(filter.City));

            var cnt = q.Count();


            if(filter.sortField == "firstName" )
                if(filter.sortOrder == "desc")
                    q = q.OrderByDescending(r => r.FirstName);
                else
                    q = q.OrderBy(r => r.FirstName);

            if (filter.sortField == "lastName")
                if (filter.sortOrder == "desc")
                    q = q.OrderByDescending(r => r.LastName);
                else
                    q = q.OrderBy(r => r.LastName);

            if (filter.sortField == "title")
                if (filter.sortOrder == "desc")
                    q = q.OrderByDescending(r => r.Title);
                else
                    q = q.OrderBy(r => r.Title);

            if (filter.sortField == "homephone")
                if (filter.sortOrder == "desc")
                    q = q.OrderByDescending(r => r.HomePhone);
                else
                    q = q.OrderBy(r => r.HomePhone);

            if (filter.sortField == "city")
                if (filter.sortOrder == "desc")
                    q = q.OrderByDescending(r => r.City);
                else
                    q = q.OrderBy(r => r.City);

            var res = new Result<EmployeeViewModel>();
            res.itemsCount = cnt;
            res.data = q.Select(r => new EmployeeViewModel
            {
                Id = r.EmployeeId,
                FirstName = r.FirstName,
                LastName = r.LastName,
                Homephone = r.HomePhone,
                Title = r.Title,
                City = r.City
            }).Skip((filter.pageIndex -1 )  * filter.pageSize  ).Take(filter.pageSize).ToArray();
            return Json(res);
        }

        public class Result<T>
        {
            public T[] data { get; set; }
            public int itemsCount { get; set; }
        }


        public class Filter
        {
            public int pageSize { get; set; }
            public int pageIndex { get; set; }
            public string sortOrder { get; set; }
            public string sortField { get; set; }

            public void Set(IQueryCollection query)
            {
                pageSize = Convert.ToInt32(query["pageSize"].ToString());
                pageIndex = Convert.ToInt32(query["pageIndex"].ToString());
                sortOrder = query.ContainsKey("sortOrder") ? query["sortOrder"].ToString() : "";
                sortField = query.ContainsKey("sortField") ? query["sortField"].ToString() : "";
            }
        }

        public class EmployeeFilter : Filter
        {
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string Title { get; set; }
            public string City { get; set; }
            public string Homephone { get; set; }
        }
        private EmployeeFilter GetFilter()
        {
            var query = Request.Query;
            var f = new EmployeeFilter();
            f.Set(query);
            f.LastName = query["lastName"].ToString();
            f.FirstName = query["firstName"].ToString();
            f.Title = query["title"].ToString();
            f.City = query["city"].ToString();
            f.Homephone = query["homephone"].ToString();
            return f;
        }
    }
}