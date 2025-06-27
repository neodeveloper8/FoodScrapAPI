using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodScrap.Domain.Entities
{
    public partial class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
