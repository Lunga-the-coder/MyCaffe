using MyCaffe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCaffe.Controllers
{
    public class DeliveryDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DeliveryDetails
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DeliveryMethod(int id)
        {
            DeliveryDetail model = new DeliveryDetail();
            model.OrderId = id;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeliveryMethod(DeliveryDetail deliveryDetail)
        {
            if (ModelState.IsValid)
            {
                Order OrderFromDb = db.Orders.Find(deliveryDetail.OrderId);
                OrderFromDb.TotalPrice = OrderFromDb.TotalPrice + Math.Round(OrderFromDb.TotalPrice * 0.15) + deliveryDetail.CalcDeliveryPrice();
                deliveryDetail.Order = OrderFromDb;
                deliveryDetail.DeliveryPrice = deliveryDetail.CalcDeliveryPrice();
                db.DeliveryDetails.Add(deliveryDetail);
                db.SaveChanges();
                return RedirectToAction("ConfirmOrder", "Orders", new { id = deliveryDetail.DeliveryDetailId });
                //return RedirectToAction("OnceOff", "Payment", new { id = deliveryDetail.OrderId });
            }

            return View(deliveryDetail);
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