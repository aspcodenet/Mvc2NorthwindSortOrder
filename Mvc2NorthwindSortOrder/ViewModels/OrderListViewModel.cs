using System;
using System.Collections.Generic;

namespace Mvc2NorthwindSortOrder.ViewModels
{
    public class OrderListViewModel
    {
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public List<OrderViewModel> Items { get; set; } = new List<OrderViewModel>();
        public class OrderViewModel
        {
            public int OrderId { get; set; }
            public string CustomerName { get; set; }
            public DateTime? OrderDate { get; set; }

        }
    }
}