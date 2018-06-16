using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YatraNepal.Models
{
    public class AdminInfo
    {
        [Column("AID")]
        public int ID { get; set; }
        public Guid AdminId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LName { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public DateTime date;
        public DateTime RegisteredDate
        {
            get { return date; }
            set { this.date = DateTime.Now; }
        }
        [Required]
        [Display(Name = "Gender")]
        public Gender Gender { get; set; }
        [Required]
        [Display(Name = "Date of Birth")]
        public DateTime Birthday { get; set; }
        public int Age { get; set; }
    }
}