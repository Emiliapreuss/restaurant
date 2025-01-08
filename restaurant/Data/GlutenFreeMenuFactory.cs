using restaurant.Models;

namespace restaurant.Data
{
    public class GlutenFreeMenuFactory : IMenuFactory
    {
        private readonly ApplicationDbContext _db;
        public GlutenFreeMenuFactory(ApplicationDbContext context)
        {
            _db = context;
        }
        public Menu CreateMenu()
        {
            Menu menu = new Menu("Gluten-Free");

            var appetizers = _db.Categories.FirstOrDefault(c => c.Name == "Appetizers")
                  ?? new Category { Name = "Appetizers", DisplayOrder = 1 };
            if (!_db.Categories.Any(c => c.Name == appetizers.Name))
            {
                _db.Categories.Add(appetizers);
            }
            menu.AddDish(new Dish { Name = "Stuffed Mushrooms", Description = "Mushrooms stuffed with vegetables", Price = 8.99m, Category = appetizers });
            menu.AddDish(new Dish { Name = "Guacamole & Rice Cakes", Description = "Fresh guacamole served with gluten-free rice cakes", Price = 6.99m, Category = appetizers });

            var mainCourses = _db.Categories.FirstOrDefault(c => c.Name == "Main Courses")
                          ?? new Category { Name = "Main Courses", DisplayOrder = 2 };
            if (!_db.Categories.Any(c => c.Name == mainCourses.Name))
            {
                _db.Categories.Add(mainCourses);
            }
            menu.AddDish(new Dish { Name = "Grilled Chicken", Description = "Grilled chicken breast with herbs and spices", Price = 12.99m, Category = mainCourses });
            menu.AddDish(new Dish { Name = "Gluten-Free Pasta", Description = "Pasta made from gluten-free ingredients", Price = 14.99m, Category = mainCourses });

            var desserts = _db.Categories.FirstOrDefault(c => c.Name == "Desserts")
                        ?? new Category { Name = "Desserts", DisplayOrder = 3 };
            if (!_db.Categories.Any(c => c.Name == desserts.Name))
            {
                _db.Categories.Add(desserts);
            }
            menu.AddDish(new Dish { Name = "Gluten-Free Brownies", Description = "Chocolate brownies made without gluten", Price = 5.99m, Category = desserts });
            menu.AddDish(new Dish { Name = "Fruit Salad", Description = "Seasonal fresh fruits", Price = 4.99m, Category = desserts });

            return menu;
        }
    }
}
