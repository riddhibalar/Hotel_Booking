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
    public class UsersController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Users
        public ActionResult Index()
        {
            if (Session["Role"]=="Admin")
            {
                return View(db.Users.ToList());
            }
            return RedirectToAction("Login","Account");
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["UserId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Users users = db.Users.Find(id);
                if (users == null)
                {
                    return HttpNotFound();
                }
                return View(users);
            }
            return RedirectToAction("Login", "Account");
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            if (Session["Role"] == "Admin")
            {
                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,FirstName,LastName,Email,Password,ConfirmPassword,ContactNumber")] Users users)
        {
            if (Session["Role"] == "Admin")
            {
                if (ModelState.IsValid)
                {
                    db.Users.Add(users);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(users);
            }
            return RedirectToAction("Login", "Account");
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Role"] == "Admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Users users = db.Users.Find(id);
                if (users == null)
                {
                    return HttpNotFound();
                }
                return View(users);
            }
            return RedirectToAction("Login", "Account");
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,FirstName,LastName,Email,Password,ConfirmPassword,ContactNumber")] Users users)
        {
            if (Session["Role"] == "Admin")
            {
                if (ModelState.IsValid)
                {
                    db.Entry(users).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(users);
            }
            return RedirectToAction("Login", "Account");
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Role"] == "Admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Users users = db.Users.Find(id);
                if (users == null)
                {
                    return HttpNotFound();
                }
                return View(users);
            }
            return RedirectToAction("Login","Account");
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["Role"] == "Admin")
            {
                Users users = db.Users.Find(id);
                db.Users.Remove(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Login","Account");
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
