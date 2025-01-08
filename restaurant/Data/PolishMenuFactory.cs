using restaurant.Models;

namespace restaurant.Data
{
    public class PolishMenuFactory :IMenuFactory
    {
        private readonly ApplicationDbContext _db;
        public PolishMenuFactory(ApplicationDbContext context)
        {
            _db = context;
        }
        public Menu CreateMenu()
        {

            Menu menu = new Menu("Polish");

            var soups = _db.Categories.FirstOrDefault(c => c.Name == "Soups")
                 ?? new Category { Name = "Soups", DisplayOrder = 1 };
            if (!_db.Categories.Any(c => c.Name == soups.Name))
            {
                _db.Categories.Add(soups);
            }
            menu.AddDish(new Dish { Name = "Żurek",Description= "Sour soup with rye starter, sausage, and egg",Price = 12.50m, Category = soups });
            menu.AddDish(new Dish { Name = "Barszcz Czerwony", Description = "Traditional beetroot borscht", Price = 10.00m, Category = soups });

            var mainCourses = _db.Categories.FirstOrDefault(c => c.Name == "Main Courses")
                          ?? new Category { Name = "Main Courses", DisplayOrder = 2 };
            if (!_db.Categories.Any(c => c.Name == mainCourses.Name))
            {
                _db.Categories.Add(mainCourses);
            }
            menu.AddDish(new Dish { Name = "Schabowy", Description = "Pork cutlet with potatoes and sauerkraut",Price = 25.99m, Category = mainCourses });
            menu.AddDish(new Dish { Name = "Pierogi Ruskie", Description = "Dumplings with cheese and potatoes", Price = 18.99m, Category = mainCourses });

            var desserts = _db.Categories.FirstOrDefault(c => c.Name == "Desserts")
                        ?? new Category { Name = "Desserts" , DisplayOrder = 3};
            if (!_db.Categories.Any(c => c.Name == desserts.Name))
            {
                _db.Categories.Add(desserts);
            }
            menu.AddDish(new Dish { Name = "Sernik", Description = "Classic Polish cheesecake", Price = 14.00m, Category = desserts });
            menu.AddDish(new Dish { Name = "Szarlotka", Description = "Apple pie served warm", Price = 13.50m, Category = desserts });
            return menu;
        }
    }
}
