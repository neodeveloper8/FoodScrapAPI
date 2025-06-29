using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodScrap.Domain.Entities;

namespace FoodScrap.Domain.Interfaces
{
    public interface IRestaurantRepository : IGenericRepository<Restaurant>
    {
        Task<List<Restaurant>> GetAllWithCategoryAsync();
        Task<Restaurant?> GetByIdWithCategoryAsync(Guid id);

    }
}
