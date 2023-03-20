using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyCaffe.Models;
using System.Data.Entity;

namespace MyCaffe.Controllers
{
    public class OrderItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrderItems
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var orderItems = db.OrderItems
                .Include(f => f.FoodItem)
                .Where(x => x.UserId == userId)
                .Where(x => x.OrderId == 0);
            return View(orderItems.ToList());
        }

        //Add to Cart
        public ActionResult AddToCart(int id)
        {
            //Gets the matching fooditem
            var foodItemfromDb = db.FoodItems.Find(id);
            //Gets the logged in User's ID
            string userId = User.Identity.GetUserId();

            //Creates a new OrderItem/CartItem
            var newOrderItem = new OrderItem();
            newOrderItem.FoodItem = foodItemfromDb;
            newOrderItem.Quantity = 1;
            newOrderItem.TotalPrice = newOrderItem.CalcItemTotalPrice(foodItemfromDb.Price);
            newOrderItem.UserId = userId;

            db.OrderItems.Add(newOrderItem);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult RemoveFromCart(int id)
        {
            //Gets the matching orderItem
            var orderItemfromDb = db.OrderItems.Find(id);

            db.OrderItems.Remove(orderItemfromDb);
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