using restaurant.Models;

namespace restaurant.Data
{
    public class VeganMenuFactory : IMenuFactory
    {
        private readonly ApplicationDbContext _db;
        public VeganMenuFactory(ApplicationDbContext context)
        {
            _db = context;
        }
        public Menu CreateMenu()
        {
            Menu menu = new Menu("Vegan");

            var soups = _db.Categories.FirstOrDefault(c => c.Name == "Soups")
               ?? new Category { Name = "Soups", DisplayOrder = 1 };
            if (!_db.Categories.Contains(soups))
            {
                _db.Categories.Add(soups); 
            }
            menu.Dishes.Add(new Dish
            {
                Name = "Tomato Soup",
                Description = "A creamy, rich tomato soup with herbs",
                Price = 9.50m,
                Category = soups
            });
            menu.Dishes.Add(new Dish
            {
                Name = "Lentil Soup",
                Description = "Hearty soup made with lentils, carrots, and celery",
                Price = 10.00m,
                Category = soups
            });

            
            var mainCourses = _db.Categories.FirstOrDefault(c => c.Name == "Main Courses")
                             ?? new Category { Name = "Main Courses" , DisplayOrder = 2 };
            if (!_db.Categories.Contains(mainCourses))
            {
                _db.Categories.Add(mainCourses);
            }
            menu.Dishes.Add(new Dish
            {
                Name = "Vegan Burger",
                Description = "Plant-based burger patty with lettuce, tomato, and vegan mayo",
                Price = 14.99m,
                Category = mainCourses
            });
           menu.Dishes.Add(new Dish
            {
                Name = "Tofu Stir-Fry",
                Description = "Stir-fried tofu with vegetables in soy sauce",
                Price = 16.99m,
                Category = mainCourses
            });

            
            var desserts = _db.Categories.FirstOrDefault(c => c.Name == "Desserts")
                          ?? new Category { Name = "Desserts", DisplayOrder = 3 };
            if (!_db.Categories.Contains(desserts))
            {
                _db.Categories.Add(desserts);
            }
            menu.Dishes.Add(new Dish
            {
                Name = "Vegan Chocolate Cake",
                Description = "Rich and moist chocolate cake without any animal products",
                Price = 12.00m,
                Category = desserts
            });
            menu.Dishes.Add(new Dish
            {
                Name = "Fruit Sorbet",
                Description = "Refreshing sorbet made with fresh seasonal fruits",
                Price = 8.50m,
                Category = desserts
            });
            return menu;
        }
    }
}
