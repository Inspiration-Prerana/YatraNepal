using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YatraNepal.Models
{
    public class LatLog
    {
        public int Id { get; set;}
        public float Lat { get; set; }
        public float Lon { get; set; }
        public float LatOffset { get; set; }
        public float LonOffset { get; set; }

    }
}