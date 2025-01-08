using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using restaurant.Models;

namespace restaurant.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.Migrate();

            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Soups", DisplayOrder = 1 },
                    new Category { Name = "Appetizers", DisplayOrder = 1 },
                    new Category { Name = "Main Courses", DisplayOrder = 2 },
                    new Category { Name = "Desserts", DisplayOrder = 3 }
                };

                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            if (!context.Menus.Any())
            {
                List<IMenuFactory> menuFactories = new List<IMenuFactory>
                {
                    new ItalianMenuFactory(context),
                    new PolishMenuFactory(context),
                    new VeganMenuFactory(context),
                    new GlutenFreeMenuFactory(context)
                };

                foreach (var factory in menuFactories)
                {
                    var menu = factory.CreateMenu();

                    context.Menus.Add(menu);
                }
                context.SaveChanges();
            }

            if (!context.Events.Any())
            {
                var events = new List<Event>
            {
                new Event
                {
                    Name = "Wine Tasting Evening",
                    Description = "Enjoy a selection of fine wines from around the world.",
                    EventDate = new DateTime(2024, 2, 14, 19, 0, 0), // Valentine's Day
                    MenuId = context.Menus.First(m => m.Name == "Italian").Id
                },
                new Event
                {
                    Name = "Live Jazz Night",
                    Description = "Experience live jazz music performed by talented musicians.",
                    EventDate = new DateTime(2024, 3, 1, 20, 0, 0),
                     MenuId = context.Menus.First(m => m.Name == "Vegan").Id
                },
                new Event
                {
                    Name = "Cooking Workshop",
                    Description = "Join our chef for a hands-on cooking workshop.",
                    EventDate = new DateTime(2024, 4, 10, 15, 0, 0),
                    MenuId = context.Menus.First(m => m.Name == "Polish").Id
                },
                
            };
                context.Events.AddRange(events);
                context.SaveChanges();
            }
           
        }
    }
}
