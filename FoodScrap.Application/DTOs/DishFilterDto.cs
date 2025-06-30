using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodScrap.Application.DTOs
{
    public class DishFilterDto
    {
        public string? Name { get; set; }

        public string? Type { get; set; }
        public Guid? RestaurantId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool OrderByPriceAsc { get; set; } = true;

        public Guid? CategoryId { get; set; }



    }
}
