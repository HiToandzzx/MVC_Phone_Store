using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HSMobile.Models;

namespace HSMobile.Controllers
{
    public class UserAccountsController : Controller
    {
        private CNPM_SHOPDTEntities db = new CNPM_SHOPDTEntities();

        public bool CheckUser(string username, string password)
        {
            var kq = db.UserAccounts.Where(x => x.Email == username && x.Password == password).ToList();
            //string hoTen = kq.First().HoTen;
            if (kq.Count() > 0)
            {
                Session["UserName"] = kq.First().UserName;
                return true;
            }
            else
            {
                Session["UserName"] = null;
                return false;
            }
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserAccount user)
        {
            if (ModelState.IsValid)
            {
                if (CheckUser(user.Email, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.Email, true);
                    // Redirect dựa trên loại tài khoản
                    if (user.Email == "admin@gmail.com")
                    {
                        return RedirectToAction("Index_Admin", "Phones");
                    }
                    else if (user.Email == "staff@gmail.com")
                    {
                        return RedirectToAction("Index_Staff", "Phones");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Phones");
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Tài khoản hoặc mật khẩu không chính xác";
                }
            }
            return View(user);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Index", "Phones");
        }

        // GET: UserAccounts
        public ActionResult Index()
        {
            return View(db.UserAccounts.ToList());
        }

        // GET: UserAccounts/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccount userAccount = db.UserAccounts.Find(id);
            if (userAccount == null)
            {
                return HttpNotFound();
            }
            return View(userAccount);
        }

        string LayMaAcc()
        {
            var maMax = db.UserAccounts.ToList().Select(n => n.ID).Max();
            int id = int.Parse(maMax.Substring(1)) + 1;
            string A = String.Concat("0", id.ToString());
            return "A" + A.Substring(id.ToString().Length - 1);
        }

        // CREATE ADMIN
        public ActionResult Create_Admin()
        {
            ViewBag.PhonesID = LayMaAcc();
            return View();
        }

        // POST: UserAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_Admin([Bind(Include = "ID,UserName,Email,Admin,Password,AddressUser")] UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                userAccount.ID = LayMaAcc();
                db.UserAccounts.Add(userAccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userAccount);
        }

        public ActionResult Create()
        {
            ViewBag.PhonesID = LayMaAcc();
            return View();
        }

        // POST: UserAccounts_63135741/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Email,Admin,Password,UserName,AddressUser")] UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                userAccount.ID = LayMaAcc();
                db.UserAccounts.Add(userAccount);
                db.SaveChanges();
                return RedirectToAction("Login", "UserAccounts");
            }

            return View(userAccount);
        }

        // GET: UserAccounts/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccount userAccount = db.UserAccounts.Find(id);
            if (userAccount == null)
            {
                return HttpNotFound();
            }
            return View(userAccount);
        }

        // POST: UserAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserName,Email,Admin,Password,AddressUser")] UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userAccount);
        }

        // GET: UserAccounts/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccount userAccount = db.UserAccounts.Find(id);
            if (userAccount == null)
            {
                return HttpNotFound();
            }
            return View(userAccount);
        }

        // POST: UserAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UserAccount userAccount = db.UserAccounts.Find(id);
            db.UserAccounts.Remove(userAccount);
            db.SaveChanges();
            return RedirectToAction("Index");
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
