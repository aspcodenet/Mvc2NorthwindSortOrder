using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Mvc2NorthwindSortOrder.ViewModels
{


    public class PagingViewModel
    {
        //https://gist.github.com/kottenator/9d936eb3e4e3c3e02598
        public IEnumerable<string> GetPages
        {
            get
            {
                int delta = 2;
                int left = CurrentPage - delta;
                int right = CurrentPage + delta + 1;

                var range = new List<string>();
                for (int i = 1; i <= MaxPages;i++)
                    if(i == 1 || i == MaxPages || (i >= left && i < right ))
                        range.Add(i.ToString());

                var rangeIncludingDots = new List<string>();
                int l = 0;
                foreach (var i in range.Select(r=>Convert.ToInt32(r)))
                {
                    if (l > 0)
                    {
                        if(i - l == 2) 
                            rangeIncludingDots.Add((l+1).ToString());
                        else if (i - l != 1)
                            rangeIncludingDots.Add("...");
                    }

                    rangeIncludingDots.Add(i.ToString());
                    l = i;
                }

                return rangeIncludingDots;
            }
        }

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

    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }

        public string ShipName { get; set; }

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