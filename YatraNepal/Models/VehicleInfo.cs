using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YatraNepal.Models
{
    public class VehicleInfo
    {
        [Column("BID")]
        public int ID { get; set; }
        [Required]
        public string VehicleId { get; set; }
        [Required]
        public string OwnerName { get; set; }
        public string OwnerTel { get; set; }
        public string OwnerAddr { get; set; }
        public int? SimNo { get; set; }
        [Display(Name = "Vehicle Type")]
        public VType VehType { get; set; }
        public DateTime date;
        public DateTime RegisteredDate
        {
            get { return date; }
            set { this.date = DateTime.Now; }
        }
        public virtual ICollection<Route> Route { get; set; }
        public virtual ICollection<Trip> Trip { get; set; }
        public virtual ICollection<VehicleManage> VManage { get; set; }

        public enum VType
        {
            Bus,
            Micro,
            Van,
            Jeep,
            Car
        }
    }

}