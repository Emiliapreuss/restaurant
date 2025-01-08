using System.ComponentModel.DataAnnotations;

namespace restaurant.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public int DisplayOrder { get; set; }
        public ICollection<Dish> Dishes { get; set; } = new List<Dish>();
    }
}
