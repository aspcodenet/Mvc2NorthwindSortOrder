﻿using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mvc2NorthwindSortOrder.ViewModels
{


    public class PagingViewModel
    {
        public int PageSize { get; set; }
        public IEnumerable<SelectListItem> PageSizeOptions
        {
            get
            {
                return new[]
                {
                    new SelectListItem("Visa 20", "20"),
                    new SelectListItem("Visa 30", "30"),
                    new SelectListItem("Visa 50", "50")
                };
            }
        }
        public int CurrentPage { get; set; }
        public int MaxPages { get; set; }

        public bool ShowPrevButton
        {
            get { return CurrentPage > 1; }
        }

        //NÄR SKA VI INTE BISA NEXY BUTTON??
        //När vi står på sista sidan
        public bool ShowNextButton
        {
            get { return CurrentPage < MaxPages; }
        }
    }

    public class OrderListViewModel
    {
        public PagingViewModel PagingViewModel { get; set; } = new  PagingViewModel();
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