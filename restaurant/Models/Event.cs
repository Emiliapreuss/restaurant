using System.ComponentModel.DataAnnotations;

namespace restaurant.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }

        public int MenuId { get; set; } 
        public Menu Menu { get; set; } 

    }
}
