using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YatraNepal.Models
{
    public class Location
    {

        [Column("LID")]
        public int ID { get; set; }
        [Display(Name = "Location Name")]
        public string strAddress { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
       
        public virtual ICollection<Ticket> Ticket { get; set; }
        public virtual ICollection<Route> Route { get; set; }
        public virtual ICollection<Trip> Trip { get; set; }
    }
}