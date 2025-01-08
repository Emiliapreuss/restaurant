using System.ComponentModel.DataAnnotations;

namespace restaurant.Models
{
    public class Menu
    {
        [Key]
        public int Id { get; set; } 
        public string Name { get; set; }
        public ICollection<Dish> Dishes { get; set; } = new List<Dish>();

        public ICollection<Event> Events { get; set; } = new List<Event>();

        public Menu(string name)
        {
            Name = name;
        }

        public void AddDish(Dish dish)
        {
            Dishes.Add(dish);
        }
    }
}
