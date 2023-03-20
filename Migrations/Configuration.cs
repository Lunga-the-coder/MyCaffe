namespace MyCaffe.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MyCaffe.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MyCaffe.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MyCaffe.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //Seed Admin Users
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role); 
            }

            if (!context.Users.Any(u => u.UserName == "admin@gmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    FirstName = "Admin",
                    Surname = "User",
                    EmailConfirmed = true
                };

                manager.Create(user, "@BreakZone2022!");
                manager.AddToRole(user.Id, "Admin");
            }

            //Seed Categories

            context.Categories.AddOrUpdate(x => x.CategoryId,
                new Category() { CategoryId = 1, CategoryName = "Sandwiches"},
                 new Category() { CategoryId = 2, CategoryName = "Burgers" },
                  new Category() { CategoryId = 3, CategoryName = "Wraps" },
                   new Category() { CategoryId = 4, CategoryName = "Sides" },
                    new Category() { CategoryId = 6, CategoryName = "Cold Drinks" },
                     new Category() { CategoryId = 7, CategoryName = "Hot Drinks" }
            );

            // SEED FOODITEMS
            context.FoodItems.AddOrUpdate(x => x.Name,
                new FoodItem() { Name = "Cheese & Tomato", Price = 23, Quantity = 15, CategoryId = 1, ImageUrl = "177.jpg" },
                new FoodItem() { Name = "Chicken & Mayo", Price = 45, Quantity = 10, CategoryId = 1, ImageUrl = "178.jpg" },
                new FoodItem() { Name = "Chicken Burger", Price = 45, Quantity = 20, CategoryId = 2, ImageUrl = "179.jpg" },
                new FoodItem() { Name = "Mutton Burger", Price = 45, Quantity = 20, CategoryId = 2, ImageUrl = "180.jpg" },
                new FoodItem() { Name = "Beef Burger", Price = 45, Quantity = 20, CategoryId = 2, ImageUrl = "181.jpg" },
                new FoodItem() { Name = "Chicken Wrap + Chips", Price = 50, Quantity = 25, CategoryId = 3, ImageUrl = "182.jpg" },
                new FoodItem() { Name = "Avo & Halloumi Wrap", Price = 55, Quantity = 25, CategoryId = 3, ImageUrl = "183.jpg" },
                new FoodItem() { Name = "Lamb Wrap + Chips", Price = 57, Quantity = 25, CategoryId = 3, ImageUrl = "184.jpg" },
                new FoodItem() { Name = "Plain Fries", Price = 25, Quantity = 40, CategoryId = 4, ImageUrl = "185.jpg" },
                new FoodItem() { Name = "Masala Fries", Price = 25, Quantity = 30, CategoryId = 4, ImageUrl = "186.jpg" },
                new FoodItem() { Name = "Cheesy Fries", Price = 30, Quantity = 40, CategoryId = 4, ImageUrl = "187.jpg" },
                new FoodItem() { Name = "Chili Cheese Fries", Price = 35, Quantity = 30, CategoryId = 4, ImageUrl = "188.jpg" },
                new FoodItem() { Name = "Water", Price = 10, Quantity = 75, CategoryId = 6, ImageUrl = "189.jpg" },
                new FoodItem() { Name = "Coke", Price = 17, Quantity = 80, CategoryId = 6, ImageUrl = "190.jpg" },
                new FoodItem() { Name = "Sprite", Price = 17, Quantity = 50, CategoryId = 6, ImageUrl = "191.jpg" },
                new FoodItem() { Name = "Juice (Assorted)", Price = 15, Quantity = 40, CategoryId = 6, ImageUrl = "192.jpg" },
                new FoodItem() { Name = "Milkshakes (Assorted)", Price = 35, Quantity = 20, CategoryId = 6, ImageUrl = "193.jpg" },
                new FoodItem() { Name = "Tea", Price = 20, Quantity = 30, CategoryId = 7, ImageUrl = "194.jpg" },
                new FoodItem() { Name = "Coffee", Price = 25, Quantity = 30, CategoryId = 7, ImageUrl = "195.jpg" },
                new FoodItem() { Name = "Cappuccino", Price = 27, Quantity = 20, CategoryId = 7, ImageUrl = "196.jpg" },
                new FoodItem() { Name = "Hot Chocolate", Price = 27, Quantity = 30, CategoryId = 7, ImageUrl = "197.jpg" }            
                
                );


        }
    }
}
