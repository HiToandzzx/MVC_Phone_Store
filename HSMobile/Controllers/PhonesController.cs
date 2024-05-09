using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HSMobile.Models;

namespace HSMobile.Controllers
{
    public class PhonesController : Controller
    {
        private CNPM_SHOPDTEntities db = new CNPM_SHOPDTEntities();

        // GET: Phones
        public ActionResult Index(string PhonesName = "", string KindID = "", string priceRange = "", string RAM = "", string DL = "", string Pin = "")
        {
            // TÌM THEO TÊN
            ViewBag.PhonesName = PhonesName;

            // TÌM THEO LOẠI
            ViewBag.KindID = new SelectList(db.KindOfPhones, "KindID", "NameOfKind");

            // TÌM THEO GIÁ
            var priceRanges = new List<SelectListItem>
                {
                    new SelectListItem { Value = "<2000000", Text = "< 2 triệu" },
                    new SelectListItem { Value = "2000000-7000000", Text = "2 - 7 triệu" },
                    new SelectListItem { Value = "700000-13000000", Text = "7 - 13 triệu" },
                    new SelectListItem { Value = "1300000-20000000", Text = "13 - 20 triệu" },
                    new SelectListItem { Value = ">20000000", Text = "> 20 triệu" }
                };

            ViewBag.PriceRanges = new SelectList(priceRanges, "Value", "Text", priceRange);

            // Khởi tạo câu lệnh SQL để thực thi thủ tục (stored procedure)
            // Câu lệnh này sẽ sử dụng các tham số giá theo `priceRange`
            int? minNewPrice = null;
            int? maxNewPrice = null;

            if (priceRange == "<2000000")
            {
                maxNewPrice = 2000000;
            }
            else if (priceRange == "2000000-7000000")
            {
                minNewPrice = 2000000;
                maxNewPrice = 7000000;
            }
            else if (priceRange == "700000-13000000")
            {
                minNewPrice = 7000000;
                maxNewPrice = 13000000;
            }
            else if (priceRange == "1300000-20000000")
            {
                minNewPrice = 13000000;
                maxNewPrice = 20000000;
            }
            else if (priceRange == ">20000000")
            {
                minNewPrice = 20000000;
            }

            // TÌM THEO RAM
            var ramOptions = new List<string> { "4 GB", "6 GB", "8 GB", "12 GB", "16 GB" }; 
            ViewBag.RamOptions = new SelectList(ramOptions, RAM);

            // TÌM THEO DUNG LƯỢNG
            var dlOptions = new List<string> { "64 GB", "128 GB", "256 GB", "512 GB", "1 TB" }; 
            ViewBag.DlOptions = new SelectList(dlOptions, DL);

            // KHỞI TẠO
            var sqlQuery = "Find_Phone @PhonesName = @p0, @KindID = @p1, @MinNewPrice = @p2, @MaxNewPrice = @p3, @RAM = @p4, @DL = @p5, @Pin = @p6";
            var phones = db.Phones.SqlQuery(sqlQuery, PhonesName, KindID, minNewPrice, maxNewPrice, RAM, DL, Pin);

            if (!phones.Any())
            {
                ViewBag.TB = "Không có thông tin tìm kiếm.";
            }

            return View(phones.ToList());
        }

        // INDEX ADMIN
        public ActionResult Index_Admin()
        {
            var phones = db.Phones.Include(p => p.KindOfPhone);
            return View(phones.ToList());
        }

        // INDEX STAFF
        public ActionResult Index_Staff()
        {
            var phones = db.Phones.Include(p => p.KindOfPhone);
            return View(phones.ToList());
        }

        // GET: Phones/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phone phone = db.Phones.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            return View(phone);
        }

        string LayMaPhone()
        {
            var maMax = db.Phones.ToList().Select(n => n.PhonesID).Max();
            int id = int.Parse(maMax.Substring(1)) + 1;
            string P = String.Concat("0", id.ToString());
            return "P" + P.Substring(id.ToString().Length - 1);
        }

        // CREATE ADMIN
        public ActionResult Create()
        {
            ViewBag.PhonesID = LayMaPhone();
            ViewBag.KindID = new SelectList(db.KindOfPhones, "KindID", "NameOfKind");
            return View();
        }

        // POST: Phones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PhonesID,PicPhone,PhonesName,KindID,Chip,Ram,DL,CameraSau,CameraTruoc,Pin,OldPrice,Per,NewPrice")] Phone phone)
        {
            //System.Web.HttpPostedFileBase Avatar;
            var imgPhone = Request.Files["pic-phone"];
            //Lấy thông tin từ input type=file có tên Avatar
            string postedFileName = System.IO.Path.GetFileName(imgPhone.FileName);
            //Lưu hình đại diện về Server
            var path = Server.MapPath("/Images/Phones/" + postedFileName);
            imgPhone.SaveAs(path);

            if (ModelState.IsValid)
            {
                phone.PhonesID = LayMaPhone();
                phone.PicPhone = postedFileName;
                db.Phones.Add(phone);
                db.SaveChanges();
                return RedirectToAction("Index_Admin");
            }

            ViewBag.KindID = new SelectList(db.KindOfPhones, "KindID", "NameOfKind", phone.KindID);
            return View(phone);
        }

        // CREATE STAFF
        public ActionResult Create_Staff()
        {
            ViewBag.PhonesID = LayMaPhone();
            ViewBag.KindID = new SelectList(db.KindOfPhones, "KindID", "NameOfKind");
            return View();
        }

        // POST: Phones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_Staff([Bind(Include = "PhonesID,PicPhone,PhonesName,KindID,Chip,Ram,DL,CameraSau,CameraTruoc,Pin,OldPrice,Per,NewPrice")] Phone phone)
        {
            //System.Web.HttpPostedFileBase Avatar;
            var imgPhone = Request.Files["pic-phone"];
            //Lấy thông tin từ input type=file có tên Avatar
            string postedFileName = System.IO.Path.GetFileName(imgPhone.FileName);
            //Lưu hình đại diện về Server
            var path = Server.MapPath("/Images/Phones/" + postedFileName);
            imgPhone.SaveAs(path);

            if (ModelState.IsValid)
            {
                phone.PhonesID = LayMaPhone();
                phone.PicPhone = postedFileName;
                db.Phones.Add(phone);
                db.SaveChanges();
                return RedirectToAction("Index_Staff");
            }

            ViewBag.KindID = new SelectList(db.KindOfPhones, "KindID", "NameOfKind", phone.KindID);
            return View(phone);
        }

        // EDIT ADMIN
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phone phone = db.Phones.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            ViewBag.KindID = new SelectList(db.KindOfPhones, "KindID", "NameOfKind", phone.KindID);
            return View(phone);
        }

        // POST: Phones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PhonesID,PicPhone,PhonesName,Chip,Ram,DL,CameraSau,CameraTruoc,Pin,OldPrice,Per,NewPrice,KindID")] Phone phone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index_Admin");
            }
            ViewBag.KindID = new SelectList(db.KindOfPhones, "KindID", "NameOfKind", phone.KindID);
            return View(phone);
        }

        // EDIT STAFF
        public ActionResult Edit_Staff(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phone phone = db.Phones.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            ViewBag.KindID = new SelectList(db.KindOfPhones, "KindID", "NameOfKind", phone.KindID);
            return View(phone);
        }

        // POST: Phones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Staff([Bind(Include = "PhonesID,PicPhone,PhonesName,Chip,Ram,DL,CameraSau,CameraTruoc,Pin,OldPrice,Per,NewPrice,KindID")] Phone phone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit");
            }
            ViewBag.KindID = new SelectList(db.KindOfPhones, "KindID", "NameOfKind", phone.KindID);
            return View(phone);
        }

        // DELETE ADMIN
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phone phone = db.Phones.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            return View(phone);
        }

        // POST: Phones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Phone phone = db.Phones.Find(id);
            db.Phones.Remove(phone);
            db.SaveChanges();
            return RedirectToAction("Index_Admin");
        }

        // DELETE STAFF
        public ActionResult Delete_Staff(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phone phone = db.Phones.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            return View(phone);
        }

        // POST: Phones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed_Staff(string id)
        {
            Phone phone = db.Phones.Find(id);
            db.Phones.Remove(phone);
            db.SaveChanges();
            return RedirectToAction("Index_Staff");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Cart()
        {
            List<CartItem> cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();
            return View(cart);
        }

        public ActionResult AddToCart(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Phone phone = db.Phones.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }

            // Assuming you have a session-based cart stored as a list of CartItems
            List<CartItem> cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();

            // Check if the item is already in the cart
            CartItem existingItem = cart.FirstOrDefault(item => item.PhonesID == id);

            if (existingItem != null)
            {
                // If the item is already in the cart, increase its quantity
                existingItem.Quantity++;
            }
            else
            {
                // If not, add it to the cart
                cart.Add(new CartItem
                {
                    PicPhone = phone.PicPhone,
                    PhonesID = phone.PhonesID,
                    PhonesName = phone.PhonesName,
                    Quantity = 1,
                    Price = phone.NewPrice
                });
            }

            // Store the updated cart back in the session
            Session["Cart"] = cart;

            return RedirectToAction("Index");
        }

        // GET: Foods_63135741/EditCartItem/5
        public ActionResult EditCartItem(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<CartItem> cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();
            CartItem itemToEdit = cart.FirstOrDefault(item => item.PhonesID == id);

            if (itemToEdit == null)
            {
                return HttpNotFound();
            }

            return View(itemToEdit);
        }

        // POST: Foods_63135741/EditCartItem/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCartItem(CartItem editedItem)
        {
            if (ModelState.IsValid)
            {
                List<CartItem> cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();
                CartItem itemToEdit = cart.FirstOrDefault(item => item.PhonesID == editedItem.PhonesID);

                if (itemToEdit != null)
                {
                    itemToEdit.Quantity = editedItem.Quantity;
                    // Add additional logic if needed (e.g., price changes, etc.)

                    // Store the updated cart back in the session
                    Session["Cart"] = cart;

                    return RedirectToAction("Cart");
                }
            }

            return View(editedItem);
        }


        // GET: Foods_63135741/DeleteCartItem/5
        public ActionResult DeleteCartItem(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<CartItem> cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();
            CartItem itemToDelete = cart.FirstOrDefault(item => item.PhonesID == id);

            if (itemToDelete == null)
            {
                return HttpNotFound();
            }

            return View(itemToDelete);
        }

        [HttpPost, ActionName("DeleteCartItem")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirme(string id)
        {
            List<CartItem> cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();
            CartItem itemToDelete = cart.FirstOrDefault(item => item.PhonesID == id);

            if (itemToDelete != null)
            {
                // Remove the item from the cart
                cart.Remove(itemToDelete);

                // Store the updated cart back in the session
                Session["Cart"] = cart;

                return RedirectToAction("Cart");
            }

            return HttpNotFound();
        }

        public ActionResult Checkout()
        {
            List<CartItem> cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();

            cart.Clear();

            Session["Cart"] = cart;

            return RedirectToAction("ThankYou");
        }

        public ActionResult ThankYou()
        {
            return View();
        }
    }
}
