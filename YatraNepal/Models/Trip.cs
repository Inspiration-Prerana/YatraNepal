using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YatraNepal.Models
{
    public class Trip
    {
        [Column("TripID")]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Bus")]
        public int BID { get; set; }
        [ForeignKey("BID")]
        public virtual VehicleInfo Bus { get; set; }

        [Display(Name = "Start Location")]
        public int? StartID { get; set; }
        [ForeignKey("StartID")]
        public virtual Location StartLocation { get; set; }

        [Display(Name = "End Location")]
        public int? EndID { get; set; }
        [ForeignKey("EndID")]
        public virtual Location EndLocation
        {
            get; set;
        }
        [Display(Name = "Departure Date")]
        public DateTime date;
        [Display(Name = "Departure Time")]
        public int? time { get; set; }
        public int? price { get; set; }
    }
}