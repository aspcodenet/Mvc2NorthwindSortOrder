using System.Collections.Generic;

namespace Mvc2NorthwindSortOrder.ViewModels
{
    public class CustomerListViewModel
    {
        public List<CustomerViewModel> Items { get; set; } = new List<CustomerViewModel>();
        public class CustomerViewModel
        {
            public string CustomerId { get; set; }
            public string CompanyName { get; set; }

            public string City { get; set; }
        }
    }

    public class CustomerEditViewModel
    {
        public string CustomerId { get; set; }
        public string Company { get; set; }
        public string ContactNameCapitalLetters { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }


        public bool HasDog { get; set; }
    }
}