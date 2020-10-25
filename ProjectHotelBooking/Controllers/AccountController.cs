using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectHotelBooking.Models;
using System.Security.Cryptography;
using System.Net;
using System.Data.Entity;
using System.Net.Mail;

namespace ProjectHotelBooking.Controllers
{
    public class AccountController : Controller
    {
        private MyDbContext db = new MyDbContext();
        
        public ActionResult Index()
        {
            ViewBag.files = db.Files;
            return View(db.Rooms.ToList());
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Users user)
        {
            if (ModelState.IsValid)
            {
                using (MyDbContext db = new MyDbContext())
                {
                    var IsCheck = db.Users.FirstOrDefault(email => email.Email == user.Email);
                    //var get_user = db.Users.irstOrDefault(p => p.Email == user.Email);
                    if (user.Email == null)
                    {
                        return View();
                    }
                    if (IsCheck != null)
                    {
                        ModelState.AddModelError("", "Username Already Exists");
                        ViewBag.reg = "true";
                        return View();
                    }
                    if (IsCheck == null)
                    {
                        db.Users.Add(user);
                        db.SaveChanges();
                    }
                    // else
                    //{
                    //  ViewBag.Message = "Email already exists" + user.Email;
                    // return View();
                    //}
                }
                ModelState.Clear();
                ViewBag.Message = "Successfully Registered Mr. " + user.FirstName + " " + user.LastName;

                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users user)
        {
            using(MyDbContext db=new MyDbContext())
            {
                if(user.Email==null || user.Password==null)
                {
                    ModelState.AddModelError("", "Email or Password does not match.");
                }

                var get_user = db.Users.FirstOrDefault(p => p.Email == user.Email && p.Password == user.Password);
                if(get_user!=null)
                {
                    Session["UserId"] =Convert.ToInt32(get_user.UserID);
                    Session["Email"] = get_user.Email.ToString();
                    Session["Role"] = "User";
                    if(user.Email=="admin@gmail.com" && user.Password=="Admin123")
                    {
                        Session["Role"] = "Admin";
                        return RedirectToAction("AdminLoggedIn");
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Email or Password does not match.");
                }
            }
            return View();
        }
        public ActionResult LoggedIn()
        {
            object obj = Session["UserId"];
            if (obj != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }

        }

        public ActionResult AdminLoggedIn()
        {
            object obj = Session["UserId"];
            if (obj != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult LogOff()
        {
            Session["UserId"] = null;
            Session["Email"] = null;
            Session["Role"] = null;
            return RedirectToAction("Index");
        }

        public ActionResult Reservation()
        {
            //Reservation vmDemo = new Reservation();
            //vmDemo.Rooms = db.Rooms.ToList();
            ViewBag.RoomsId =new SelectList(db.Rooms,"RoomsId","Type");
            return View();
        }
        [HttpPost]
        public ActionResult Reservation(Orders orders)
        {
            if (Session["UserId"] != null)
            {
                if (ModelState.IsValid)
                {
                    var rid = orders.RoomsId;
                    var temp = db.Rooms.SingleOrDefault(p => p.RoomsId == rid);
                    if (Convert.ToInt32(temp.Count) >= Convert.ToInt32(orders.number_of_rooms))
                    {
                        var nrc = Convert.ToInt32(temp.Count) - Convert.ToInt32(orders.number_of_rooms);
                        var temp1 = db.Rooms.SingleOrDefault(p => p.RoomsId == rid);
                        temp1.Count = nrc;
                    }
                    else
                    {
                        TempData["Data"] = "Fail";
                        return RedirectToAction("Index","Account");
                    }
                    TempData["Data"]= "Success";
                    orders.UserID = Convert.ToInt32(Session["UserId"]);
                    db.Orders.Add(orders);
                    db.SaveChanges();

                    var senderEmail = "rbalar26@gmail.com";
                    var receiverEmail = orders.email;
                    var password = "riddhi395006";
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port=587,
                        EnableSsl=true,
                        DeliveryMethod=SmtpDeliveryMethod.Network,
                        UseDefaultCredentials=false,
                        Credentials=new System.Net.NetworkCredential(senderEmail,password)
                    };
                    using(var mess=new MailMessage(senderEmail,receiverEmail)
                    {
                        Subject="SUCCESSFUL BOOKING",
                        Body="We successfully booked your stay:       HERE IS YOUR ORDER SUMMERY:       NAME: "+orders.name+ "       CONTACT NO: "+orders.contactno+"     CHECKIN DATE: "+orders.checkin+"        CHECKOUT DATE: "+orders.checkout+"       TYPE:"+temp.Type 
                    })
                    {
                        smtp.Send(mess);
                    }

                    return RedirectToAction("Index", "Account");
                }
                ViewBag.RoomsId = new SelectList(db.Rooms, "RoomsId", "Type");
                return View();
            }
            return RedirectToAction("Login", "Account");
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Edit(int? id)
        {
            int uid =Convert.ToInt32(Session["UserId"]);
            if (Session["UserId"] != null)
            {
                if(uid==null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }                
                Users users = db.Users.Find(uid);
                if (users == null)
                {
                    return HttpNotFound();
                }
                return View(users);
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,FirstName,LastName,Email,Password,ConfirmPassword,ContactNumber")] Users users)
        {
            if (Session["UserId"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(users).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Manage","Account");
                }
                return View(users);
            }
            return RedirectToAction("Login", "Account");
        }
        [AllowAnonymous]
        public ActionResult Manage(int? id)
        {
            int uid = Convert.ToInt32(Session["UserId"]);
            if (Session["UserId"] != null)
            {
                if (uid == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Users users = db.Users.Find(uid);
                if (users == null)
                {
                    return HttpNotFound();
                }
                return View(users);
            }
            return RedirectToAction("Login", "Account");
        }
        public ActionResult mybookings()
        {
            int uid=Convert.ToInt32(Session["UserId"]);
            if (Session["UserId"] != null)
            {
                if (uid == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                List<Orders> orders = db.Orders.Where(p=>p.UserID==uid).ToList();
                if (orders == null)
                {
                    return HttpNotFound();
                }
                return View(orders);
            }
            return RedirectToAction("Login", "Account");
        }
    }
}