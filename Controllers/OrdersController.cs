using Microsoft.AspNet.Identity;
using MyCaffe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;

namespace MyCaffe.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Ongoing()
        {
            var userId = User.Identity.GetUserId();

            if (User.IsInRole("Admin"))
            {
                var orders = db.Orders.Where(x => x.Status != Order.OrderStatus.Delivered);
                return View(orders.ToList());
            }
            else if (User.IsInRole("Student"))
            {
                var orders = db.Orders.Where(x => x.UserId == userId).Where(x => x.Status != Order.OrderStatus.Delivered);
                return View(orders.ToList());
            }
            else
            {
                return View();
            }
        }

        public ActionResult Past()
        {
            var userId = User.Identity.GetUserId();

            if (User.IsInRole("Admin"))
            {
                var orders = db.Orders.Where(x => x.Status == Order.OrderStatus.Delivered);
                return View(orders.ToList());
            }
            else if (User.IsInRole("Student"))
            {
                var orders = db.Orders.Where(x => x.UserId == userId).Where(x => x.Status == Order.OrderStatus.Delivered);
                return View(orders.ToList());
            }
            else
            {
                return View();
            }
        }

        public ActionResult CompleteOrder()
        {
            //Get logged in UserId
            var userId = User.Identity.GetUserId();

            //Get all cart items
            IEnumerable<OrderItem> orderItems = db.OrderItems
                .Where(x => x.UserId == userId)
                .Where(x => x.OrderId == 0);

            //Calculate total Price
            double orderTotalPrice = 0;
            foreach (var item in orderItems)
            {
                orderTotalPrice += item.TotalPrice;
            }

            //Create new Order
            var newOrder = new Order();
            newOrder.TotalPrice = orderTotalPrice;
            newOrder.UserId = userId;
            newOrder.Date = DateTime.Now.AddHours(2);
            newOrder.Time = DateTime.Now.AddHours(2);
            newOrder.Status = Order.OrderStatus.OrderPlaced;
            newOrder.EstimatedDelivery = DateTime.Now.AddHours(2).AddMinutes(30);
            db.Orders.Add(newOrder);
            db.SaveChanges();
            

            using (var db = ApplicationDbContext.Create())
            {
                //Add cart items to order
                foreach (var item in orderItems)
                {
                    OrderItem OrderItemInDb = db.OrderItems.Find(item.OrderItemId);
                    OrderItemInDb.OrderId = newOrder.OrderId;
                }

                db.SaveChanges();
            }

            //return RedirectToAction("Index");
            //return RedirectToAction("OnceOff", "Payment", new { id = newOrder.OrderId });
            SendOrderPlacedEmail(newOrder.OrderId);
            return RedirectToAction("DeliveryMethod", "DeliveryDetails", new { id = newOrder.OrderId });
        }

        public void SendOrderPlacedEmail(int id)
        {
            //Get logged in UserId
            var userId = User.Identity.GetUserId();
            //Get logged in User
            var currentUser = db.Users.Find(userId);

            //Get new order
            var newOrder = db.Orders.Find(id);

            var subject = "New Order Placed - Order #" + newOrder.OrderId;

            string body =
                $"Hello, {currentUser.FirstName} <br><br>" +
                $"Your order has successfully been placed! Please expect your order around {newOrder.EstimatedDelivery}. <br><br>" +
                "Kind Regards, <br>" +
                "The Break Zone Team :)";

            UserManager.SendEmail(userId, subject, body);
        }



        public ActionResult ConfirmOrder(int id)
        {
            var DeliveryDetailsFromDb = db.DeliveryDetails.Find(id);

            return View(DeliveryDetailsFromDb);
        }

        public ActionResult ProceedToPayment(int id)
        {
            var finalOrder = db.Orders.Find(id);

            return RedirectToAction("OnceOff", "Payment", new { id = finalOrder.OrderId });
        }

        public ActionResult Prepare(int id)
        {
            var currentOrder = db.Orders.Find(id);

            currentOrder.Status = Order.OrderStatus.Preparing;
            db.SaveChanges();

            return RedirectToAction("Ongoing", "Orders");
        }

        public ActionResult OutForDelivery(int id)
        {
            var currentOrder = db.Orders.Find(id);

            currentOrder.Status = Order.OrderStatus.OutForDelivery;
            db.SaveChanges();

            SendOutForDeliveryEmail(id);
            return RedirectToAction("Ongoing", "Orders");
        }

        public void SendOutForDeliveryEmail(int id)
        {
            //Get current order
            var currentOrder = db.Orders.Find(id);

            //Get logged in User
            var currentUser = db.Users.Find(currentOrder.UserId);
            

            var subject = "Out for Delivery - Order #" + currentOrder.OrderId;

            string body =
                $"Hello, {currentUser.FirstName} <br><br>" +
                $"Your order is currently out for delivery! Please be on the lookout. <br><br>" +
                "Kind Regards, <br>" +
                "The Break Zone Team :)";

            UserManager.SendEmail(currentOrder.UserId, subject, body);
        }

        public ActionResult Delivered(int id)
        {
            var currentOrder = db.Orders.Find(id);

            currentOrder.Status = Order.OrderStatus.Delivered;
            currentOrder.ActualDelivery = DateTime.Now.AddHours(2);
            db.SaveChanges();

            return RedirectToAction("Ongoing", "Orders");
        }

        public ActionResult Details(int id)
        {
            var DeliveryDetailsFromDb = db.DeliveryDetails.FirstOrDefault(x => x.OrderId == id);

            return View(DeliveryDetailsFromDb);
        }

        public ActionResult BackToOrder(int id)
        {
            var currentOrder = db.Orders.Find(id);

            if (currentOrder.Status == Order.OrderStatus.Delivered)
            {
                return RedirectToAction("Past");
            }
            else
            {
                return RedirectToAction("Ongoing");
            }
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