﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mvc2NorthwindSortOrder.Models
{
    public partial class ProductsByCategory
    {
        [Required]
        [StringLength(15)]
        public string CategoryName { get; set; }
        [Required]
        [StringLength(40)]
        public string ProductName { get; set; }
        [StringLength(20)]
        public string QuantityPerUnit { get; set; }
        public short? UnitsInStock { get; set; }
        public bool Discontinued { get; set; }
    }
}
