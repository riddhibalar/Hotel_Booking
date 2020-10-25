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
    public class OrdersController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            if (Session["Role"]== "Admin")
            {
                var orders = db.Orders.Include(o => o.Rooms).Include(o => o.Users);
                return View(orders.ToList());
            }
            return RedirectToAction("Index","Account");
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Role"] == "Admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Orders orders = db.Orders.Find(id);
                if (orders == null)
                {
                    return HttpNotFound();
                }
                return View(orders);
            }
            return RedirectToAction("Index", "Account");
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            if (Session["Role"] == "Admin")
            {
                ViewBag.RoomsId = new SelectList(db.Rooms, "RoomsId", "Type");
                ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName");
                return View();
            }
            return RedirectToAction("Index","Account");
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,name,contactno,email,checkin,checkout,number_of_rooms,RoomsId,UserID")] Orders orders)
        {
            if (Session["Role"]=="Admin")
            {
                if (ModelState.IsValid)
                {
                    db.Orders.Add(orders);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.RoomsId = new SelectList(db.Rooms, "RoomsId", "Type", orders.RoomsId);
                ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", orders.UserID);
                return View(orders);
            }
            return RedirectToAction("Index","Account");
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Role"] == "Admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Orders orders = db.Orders.Find(id);
                if (orders == null)
                {
                    return HttpNotFound();
                }
                ViewBag.RoomsId = new SelectList(db.Rooms, "RoomsId", "Type", orders.RoomsId);
                ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", orders.UserID);
                return View(orders);
            }
            return RedirectToAction("Index", "Account");
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,name,contactno,email,checkin,checkout,number_of_rooms,RoomsId,UserID")] Orders orders)
        {
            if (Session["Role"] == "Admin")
            {
                if (ModelState.IsValid)
                {
                    db.Entry(orders).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.RoomsId = new SelectList(db.Rooms, "RoomsId", "Type", orders.RoomsId);
                ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", orders.UserID);
                return View(orders);
            }
            return RedirectToAction("Index", "Account");
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Role"] == "Admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Orders orders = db.Orders.Find(id);
                if (orders == null)
                {
                    return HttpNotFound();
                }
                return View(orders);
            }
         return RedirectToAction("Index", "Account");
    }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["Role"] == "Admin")
            {
                Orders orders = db.Orders.Find(id);
                db.Orders.Remove(orders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Account");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
