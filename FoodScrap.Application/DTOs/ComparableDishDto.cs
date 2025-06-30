using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodScrap.Application.DTOs
{
    public class ComparableDishDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Type { get; set; }
        public string RestaurantName { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public double AverageRating { get; set; }
    }
}
