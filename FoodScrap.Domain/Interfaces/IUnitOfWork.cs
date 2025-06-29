using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodScrap.Domain.Entities;

namespace FoodScrap.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<User> Users { get; }
        IRestaurantRepository Restaurants { get; }
        IGenericRepository<RestaurantCategory> RestaurantCategories { get; }
        IGenericRepository<Dish> Dishes { get; }
        IGenericRepository<Review> Reviews { get; }

        Task<int> CompleteAsync();
    }
}
