using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YatraNepal.Models
{
    public class VehicleManage
    {
        public int ID { get; set; }
        public Guid AgentID { get; set; }
        public int BID { get; set; }
        [ForeignKey("BID")]
        public virtual VehicleInfo Bus { get; set; }
    }
}