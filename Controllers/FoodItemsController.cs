using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyCaffe.Models;

namespace MyCaffe.Controllers
{
    public class FoodItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FoodItems
        public ActionResult Index()
        {
            var foodItems = db.FoodItems.Include(f => f.Category);
            return View(foodItems.ToList());
        }

        // GET: FoodItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodItem foodItem = db.FoodItems.Find(id);
            if (foodItem == null)
            {
                return HttpNotFound();
            }
            return View(foodItem);
        }

        // GET: FoodItems/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: FoodItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FoodItem foodItem, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                db.FoodItems.Add(foodItem);
                db.SaveChanges();

                var uploadsDir = new DirectoryInfo(string.Format("{0}Uploads", Server.MapPath(@"\")));
                if (file != null && file.ContentLength > 0)
                {
                    string ext = file.ContentType.ToLower();

                    if (ext != "image/jpg" &&
                        ext != "image/jpeg" &&
                        ext != "image/pjpeg" &&
                        ext != "image/gif" &&
                        ext != "image/x-png" &&
                        ext != "image/png")
                    {
                        ModelState.AddModelError("", "The image was not uploaded - wrong image extension.");
                        return View("Index", foodItem);
                    }

                    string imageName = foodItem.FoodItemId + ".jpg";
                    var path = string.Format("{0}\\{1}", uploadsDir, imageName);
                    file.SaveAs(path);
                }

                FoodItem item = db.FoodItems.Find(foodItem.FoodItemId);
                item.ImageUrl = foodItem.FoodItemId + ".jpg";
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", foodItem.CategoryId);
            return View(foodItem);
        }

        // GET: FoodItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodItem foodItem = db.FoodItems.Find(id);
            if (foodItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", foodItem.CategoryId);
            return View(foodItem);
        }

        // POST: FoodItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FoodItem foodItem, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var uploadsDir = new DirectoryInfo(string.Format("{0}Uploads", Server.MapPath(@"\")));
                if (file != null && file.ContentLength > 0)
                {
                    string ext = file.ContentType.ToLower();

                    if (ext != "image/jpg" &&
                        ext != "image/jpeg" &&
                        ext != "image/pjpeg" &&
                        ext != "image/gif" &&
                        ext != "image/x-png" &&
                        ext != "image/png")
                    {
                        ModelState.AddModelError("", "The image was not uploaded - wrong image extension.");
                        return View("Index", foodItem);
                    }

                    string imageName = foodItem.FoodItemId + ".jpg";
                    var path = string.Format("{0}\\{1}", uploadsDir, imageName);
                    file.SaveAs(path);
                }

                foodItem.ImageUrl = foodItem.FoodItemId + ".jpg";
                db.Entry(foodItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", foodItem.CategoryId);
            return View(foodItem);
        }

        // GET: FoodItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodItem foodItem = db.FoodItems.Find(id);
            if (foodItem == null)
            {
                return HttpNotFound();
            }
            return View(foodItem);
        }

        // POST: FoodItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FoodItem foodItem = db.FoodItems.Find(id);
            db.FoodItems.Remove(foodItem);
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
