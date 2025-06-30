using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodScrap.Application.DTOs
{
    public class TopReviewedRestaurantDto
    {
        public string RestaurantName { get; set; } = null!;
        public int TotalReviews { get; set; }
    }
}
