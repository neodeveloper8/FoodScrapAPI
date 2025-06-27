using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodScrap.Domain.Entities
{
    public partial class Dish
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public string? Type { get; set; }

        public Guid RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; } = null!;

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }

}
