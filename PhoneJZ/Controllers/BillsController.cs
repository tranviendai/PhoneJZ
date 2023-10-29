using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PhoneJZ.App_Data;

namespace PhoneJZ.Controllers
{
    public class BillsController : Controller
    {
        private PhoneDB db = new PhoneDB();

        public ActionResult Index()
        {
            var orderProes = db.OrderProes.Include(o => o.AspNetUser);
            return View(orderProes.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderPro orderPro = db.OrderProes.Find(id);
            if (orderPro == null)
            {
                return HttpNotFound();
            }
            return View(orderPro);
        }

        public ActionResult Create()
        {
            ViewBag.userID = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,dateOrder,fullname,address,userID,totalPrice")] OrderPro orderPro)
        {
            if (ModelState.IsValid)
            {
                db.OrderProes.Add(orderPro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userID = new SelectList(db.AspNetUsers, "Id", "Email", orderPro.userID);
            return View(orderPro);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderPro orderPro = db.OrderProes.Find(id);
            if (orderPro == null)
            {
                return HttpNotFound();
            }
            ViewBag.userID = new SelectList(db.AspNetUsers, "Id", "Email", orderPro.userID);
            return View(orderPro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,dateOrder,fullname,address,userID,totalPrice")] OrderPro orderPro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderPro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userID = new SelectList(db.AspNetUsers, "Id", "Email", orderPro.userID);
            return View(orderPro);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderPro orderPro = db.OrderProes.Find(id);
            if (orderPro == null)
            {
                return HttpNotFound();
            }
            return View(orderPro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderPro orderPro = db.OrderProes.Find(id);
            db.OrderProes.Remove(orderPro);
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
