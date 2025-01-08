using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restaurant.Data;
using restaurant.Models;

namespace restaurant.Controllers
{
    public class MenuController : Controller
    {
        private readonly ApplicationDbContext _db;
        public MenuController(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public IActionResult Index()
        {
            var objMenuList = _db.Menus.ToList();
            return View(objMenuList);
        }

        public async Task<IActionResult> CheckMenu(int id)
        {
            var menu = await _db.Menus
        .Include(m => m.Dishes)
        .ThenInclude(d => d.Category)
        .FirstOrDefaultAsync(m => m.Id == id);

            if (menu == null)
            {
                return NotFound();
            }

            var groupedDishes = menu.Dishes
                .GroupBy(d => d.Category)
                .OrderBy(g => g.Key.DisplayOrder)
                .ToDictionary(
                    g => g.Key.Name,
                    g => g.ToList()
                );

            ViewBag.MenuName = menu.Name;
            ViewBag.GroupedDishes = groupedDishes;

            return View();
        }
    }
}
