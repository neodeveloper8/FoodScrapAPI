using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodScrap.Domain.Entities
{
    public partial class Restaurant
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Address { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public Guid CategoryId { get; set; }

        public virtual RestaurantCategory Category { get; set; } = null!;

        public virtual ICollection<Dish> Dishes { get; set; } = new List<Dish>();
    }

}
