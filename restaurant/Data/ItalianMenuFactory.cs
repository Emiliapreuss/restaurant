using Microsoft.EntityFrameworkCore;
using restaurant.Models;

namespace restaurant.Data
{
    public class ItalianMenuFactory : IMenuFactory
    {
        private readonly ApplicationDbContext _db;

        public ItalianMenuFactory(ApplicationDbContext context)
        {
            _db = context;
        }
        public Menu CreateMenu()
        {
            Menu menu = new Menu("Italian");

            var appetizers = _db.Categories.FirstOrDefault(c => c.Name == "Appetizers")
                        ?? new Category { Name = "Appetizers", DisplayOrder = 1 };

            if (!_db.Categories.Contains(appetizers))
            {
                _db.Categories.Add(appetizers); 
            }

            menu.Dishes.Add(
                new Dish { Name = "Bruschetta", Description = "Toasted bread with tomatoes", Price = 8.99m, Category = appetizers }
                );

            menu.Dishes.Add(
                new Dish { Name = "Garlic Bread", Description = "Freshly baked garlic bread", Price = 5.99m, Category = appetizers }
                );


            var mainCourses = _db.Categories.FirstOrDefault(c => c.Name == "Main Courses")
                          ?? new Category { Name = "Main Courses", DisplayOrder = 2 };

            if (!_db.Categories.Contains(mainCourses))
            {
                _db.Categories.Add(mainCourses);
            }

            menu.Dishes.Add(
                new Dish { Name = "Spaghetti Carbonara", Description = "Classic Italian pasta", Price = 12.99m, Category = mainCourses }
                );

            menu.Dishes.Add(
               new Dish { Name = "Margherita Pizza", Description = "Pizza with tomato, mozzarella, and basil", Price = 10.99m, Category = mainCourses }
               );

            var desserts = _db.Categories.FirstOrDefault(c => c.Name == "Desserts")
                       ?? new Category { Name = "Desserts", DisplayOrder = 3 };
            if (!_db.Categories.Contains(desserts))
            {
                _db.Categories.Add(desserts);
            }

            menu.Dishes.Add(
                new Dish { Name = "Tiramisu", Description = "Coffee-flavored Italian dessert", Price = 6.99m, Category = desserts }
            );

            menu.Dishes.Add(
                new Dish { Name = "Gelato", Description = "Italian ice cream", Price = 4.99m, Category = desserts }
            );


            return menu;
        }
    }

}
