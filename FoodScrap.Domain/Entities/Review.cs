using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodScrap.Domain.Entities
{
    public partial class Review
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid DishId { get; set; }

        public int? Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual Dish Dish { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }

}
