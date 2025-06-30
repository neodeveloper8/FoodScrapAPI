using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodScrap.Application.DTOs
{
    public class AveragePriceByCategoryDto
    {
        public string CategoryName { get; set; } = null!;
        public double AveragePrice { get; set; }
    }
}
