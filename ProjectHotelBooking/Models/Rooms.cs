using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectHotelBooking.Models
{
    public class Rooms
    {
        [Key]
        public int RoomsId { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string Type { get; set; }

        [Required]
        [Display(Name ="Price")]
        public double Price { get; set; }

        [Required]
        [Display(Name ="No of Rooms")]
        public int Count { get; set; }

        public virtual ICollection<Files> Files { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}