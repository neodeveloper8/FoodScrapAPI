using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodScrap.Domain.Entities
{
    public partial class RestaurantCategory
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
    }
}
