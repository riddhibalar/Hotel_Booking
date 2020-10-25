using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectHotelBooking.Models;

namespace ProjectHotelBooking.Controllers
{
    public class RoomsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Rooms
        public ActionResult Index()
        {
            if (Session["Role"] == "Admin")
            {
                return View(db.Rooms.ToList());
            }
            return RedirectToAction("Login","Account");
        }

        // GET: Rooms/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Role"] == "Admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Rooms rooms = db.Rooms.Include(s => s.Files).SingleOrDefault(s => s.RoomsId == id);
                if (rooms == null)
                {
                    return HttpNotFound();
                }
                return View(rooms);
            }
            return RedirectToAction("Login", "Account");
        }

        // GET: Rooms/Create
        public ActionResult Create()
        {
            if (Session["Role"] == "Admin")
            {
                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoomsId,Type,Price,Count")] Rooms rooms, HttpPostedFileBase upload)
        {
            if (Session["Role"] == "Admin")
            { 
                try
                {
                    if (ModelState.IsValid)
                    {
                        if (upload != null && upload.ContentLength > 0)
                        {
                            var avatar = new Files
                            {
                                FileName = System.IO.Path.GetFileName(upload.FileName),
                                FileType = FileType.Avatar,
                                ContentType = upload.ContentType
                            };
                            using (var reader = new System.IO.BinaryReader(upload.InputStream))
                            {
                                avatar.Content = reader.ReadBytes(upload.ContentLength);
                            }
                            rooms.Files = new List<Files> { avatar };
                        }
                        db.Rooms.Add(rooms);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                catch (RetryLimitExceededException /* dex */)
                {  
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            return View(rooms);
            }
            return RedirectToAction("Login", "Account");
        }

        // GET: Rooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Role"] == "Admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Rooms rooms = db.Rooms.Include(s => s.Files).SingleOrDefault(s => s.RoomsId == id);
                if (rooms == null)
                {
                    return HttpNotFound();
                }
                return View(rooms);
            }
            return RedirectToAction("Login", "Account");
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoomsId,Type,Price,Count")] Rooms rooms, HttpPostedFileBase upload)
        {
            if (Session["Role"] == "Admin")
            {
                if (ModelState.IsValid)
                {
                    var roomsToUpdate = db.Rooms.Find(rooms.RoomsId);
                    if (upload != null && upload.ContentLength > 0)
                    {
                        if (roomsToUpdate.Files.Any(f => f.FileType == FileType.Avatar))
                        {
                            db.Files.Remove(roomsToUpdate.Files.First(f => f.FileType == FileType.Avatar));
                        }
                        var avatar = new Files
                        {
                            FileName = System.IO.Path.GetFileName(upload.FileName),
                            FileType = FileType.Avatar,
                            ContentType = upload.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            avatar.Content = reader.ReadBytes(upload.ContentLength);
                        }
                        roomsToUpdate.Files = new List<Files> { avatar };
                    }
                    roomsToUpdate.Type = rooms.Type;
                    roomsToUpdate.Price = rooms.Price;
                    roomsToUpdate.Count = rooms.Count;
                    //db.Entry(rooms).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(rooms);
            }
            return RedirectToAction("Login", "Account");
        }

        // GET: Rooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Role"] == "Admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Rooms rooms = db.Rooms.Find(id);
                if (rooms == null)
                {
                    return HttpNotFound();
                }
                return View(rooms);
            }
            return RedirectToAction("Login", "Account");
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["Role"] == "Admin")
            {
                Rooms rooms = db.Rooms.Find(id);
                db.Rooms.Remove(rooms);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Login", "Account");
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
