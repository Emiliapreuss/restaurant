using Microsoft.AspNetCore.Mvc;
using restaurant.Data;
using restaurant.Models;
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

        public IActionResult Index()
        {
            var events = _db.Events.Include(e => e.Menu).ToList();
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
        public IActionResult Create()
        {
            ViewBag.Menus = _db.Menus.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Event newEvent)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Menus = _db.Menus.ToList();
                return View(newEvent);
            }

            _db.Events.Add(newEvent);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var @event = _db.Events.Find(id);
            if (@event == null)
            {
                return NotFound();
            }

            ViewBag.Menus = _db.Menus.ToList();
            return View(@event);
        }

        [HttpPost]
        public IActionResult Edit(int id, Event newEvent)
        {
            if (id != newEvent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(newEvent);
                    _db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(newEvent.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Menus = _db.Menus.ToList();
            return View(newEvent);
        }

        public IActionResult Delete(int id)
        {
            var eventToDel = _db.Events.Include(e => e.Menu).FirstOrDefault(e => e.Id == id);
            if (eventToDel == null)
            {
                return NotFound();
            }

            return View(eventToDel);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var eventToDel = _db.Events.Find(id);
            if (eventToDel != null)
            {
                _db.Events.Remove(eventToDel);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _db.Events.Any(e => e.Id == id);
        }
    }
}
