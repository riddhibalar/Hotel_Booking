using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectHotelBooking.Models
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }

        [Required(ErrorMessage ="Name is required!")]
        [Display(Name ="Name")]
        public string name { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Contact Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        public int contactno { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required(ErrorMessage ="Check in date is required!")]
        [Display(Name = "Date Check In")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/mm/yyyy}")]
        public string checkin { get; set; }

        [Required(ErrorMessage = "Check out date is required!")]
        [Display(Name = "Date Check Out")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/mm/yyyy}")]
        public string checkout { get; set; }

        [Required(ErrorMessage ="Number of room must not be empty")]
        [Display(Name ="Number of Rooms")]
        public int number_of_rooms { get; set; }

        public int RoomsId { get; set; }

        public int UserID { get; set; }
        public virtual Users Users { get; set; }
        public virtual Rooms Rooms { get; set; }

    }
}