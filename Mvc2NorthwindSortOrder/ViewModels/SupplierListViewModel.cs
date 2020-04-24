using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace Mvc2NorthwindSortOrder.ViewModels
{
    public class SupplierListViewModel
    {
        public string SearchWord { get; set; }
        public List<SupplierViewModel> Items { get; set; } = new List<SupplierViewModel>();
        public class SupplierViewModel
        {
            public int Id { get; set; }
            public string Company { get; set; }
            public string ContactName { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
        }
    }
}