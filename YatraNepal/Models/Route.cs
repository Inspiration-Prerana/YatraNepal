using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YatraNepal.Models
{
    public class Route
    {
        [Column("RID")]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Buses")]
        public int BID { get; set; }
        [ForeignKey("BID")]
        public virtual VehicleInfo Bus { get; set; }

        [Display(Name = "Stops")]
        public int? LID { get; set; }
        [ForeignKey("LID")]
        public virtual Location Location { get; set; }
    }
}