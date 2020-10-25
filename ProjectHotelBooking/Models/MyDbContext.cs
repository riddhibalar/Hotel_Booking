using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ProjectHotelBooking.Models
{
    public class MyDbContext:DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<Files> Files { get; set; }
        public DbSet<Orders> Orders { get; set; }
    }
}