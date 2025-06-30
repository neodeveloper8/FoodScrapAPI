using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodScrap.Application.DTOs
{
    public class TopRatedDishDto
    {
        public string DishName { get; set; } = null!;
        public string RestaurantName { get; set; } = null!;
        public double AverageRating { get; set; }
    }
}
