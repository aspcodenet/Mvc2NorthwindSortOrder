using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mvc2NorthwindSortOrder.Models
{
    public partial class Territories
    {
        public Territories()
        {
            EmployeeTerritories = new HashSet<EmployeeTerritories>();
        }

        [Key]
        [Column("TerritoryID")]
        [StringLength(20)]
        public string TerritoryId { get; set; }
        [Required]
        [StringLength(50)]
        public string TerritoryDescription { get; set; }
        [Column("RegionID")]
        public int RegionId { get; set; }

        [ForeignKey(nameof(RegionId))]
        [InverseProperty("Territories")]
        public virtual Region Region { get; set; }
        [InverseProperty("Territory")]
        public virtual ICollection<EmployeeTerritories> EmployeeTerritories { get; set; }
    }
}
