using Microsoft.AspNetCore.Mvc;
using restaurant.Data;
using Microsoft.EntityFrameworkCore;

namespace restaurant.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _db;

        public EventController(ApplicationDbContext context)
        {
            _db = context;
        }

        public async Task<IActionResult> Index()
        {
            var events = await _db.Events.Include(e => e.Menu).ToListAsync();
            return View(events);
        }

        public async Task<IActionResult> Details(int id)
        {
            var eventDetails = await _db.Events
        .Include(e => e.Menu)
        .ThenInclude(m => m.Dishes)
        .ThenInclude(d => d.Category)
        .FirstOrDefaultAsync(e => e.Id == id);

            if (eventDetails == null)
            {
                return NotFound();
            }

            var groupedDishes = eventDetails.Menu.Dishes
                .GroupBy(d => d.Category)
                .OrderBy(g => g.Key.DisplayOrder)
                .ToDictionary(
                    g => g.Key.Name,
                    g => g.ToList()
                );

            ViewBag.EventName = eventDetails.Name;
            ViewBag.Description = eventDetails.Description;
            ViewBag.MenuName = eventDetails.Menu.Name;
            ViewBag.GroupedDishes = groupedDishes;

            return View(eventDetails);
        }
    }
}
