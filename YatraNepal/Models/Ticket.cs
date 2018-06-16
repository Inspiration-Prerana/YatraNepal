using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YatraNepal.Models
{
    public class Ticket
    {
        [Column("TID")]
        public Guid ID { get; set; }
        [Required]
        [Display(Name = "Passenger")]
        public Guid UserID { get; set; }

        [Display(Name = "ID")]
        public int? TripID { get; set; }
        [ForeignKey("TripID")]
        public virtual Trip Trip { get; set; }

        [Display(Name = "Start Location")]
        public int? StartID { get; set; }
        [ForeignKey("StartID")]
        public virtual Location StartLocation { get; set; }

        [Display(Name = "End Location")]
        public int? EndID { get; set; }
        [ForeignKey("EndID")]
        public virtual Location EndLocation { get; set; }
        [Display(Name = "Bus Required on Date")]
        public DateTime? date;
        public int? SeatNo { get; set; }
        public float? Price { get; set; }
    }
}