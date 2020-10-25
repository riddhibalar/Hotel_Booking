using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectHotelBooking.Models;

namespace ProjectHotelBooking.Controllers
{
    public class FilesController : Controller
    {
        private MyDbContext db = new MyDbContext();

        public ActionResult Index(int? id)
        {
            var fileToRetrieve = db.Files.Find(id);
            return File(fileToRetrieve.Content, fileToRetrieve.ContentType);

        }
        public ActionResult Demo()
        {
            if (Session["Role"] == "Admin")
            {
                return View(db.Files.ToList());
            }
            return RedirectToAction("Login","Account");
        }
    }
}