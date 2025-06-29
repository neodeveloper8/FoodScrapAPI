using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodScrap.Domain.Entities;
using FoodScrap.Domain.Interfaces;
using FoodScrap.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FoodScrap.Infrastructure.Repositories
{
    public class RestaurantRepository : GenericRepository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(ApplicationDbContext context) : base(context) { }

        public async Task<List<Restaurant>> GetAllWithCategoryAsync()
        {
            return await _context.Restaurants
                .Include(r => r.Category)
                .ToListAsync();
        }

        public async Task<Restaurant?> GetByIdWithCategoryAsync(Guid id)
        {
            return await _context.Restaurants
                .Include(r => r.Category)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
