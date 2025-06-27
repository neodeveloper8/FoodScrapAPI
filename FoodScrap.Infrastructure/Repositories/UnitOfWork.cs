using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodScrap.Domain.Entities;
using FoodScrap.Domain.Interfaces;
using FoodScrap.Infrastructure.Context;

namespace FoodScrap.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGenericRepository<User> Users { get; }
        public IGenericRepository<Restaurant> Restaurants { get; }
        public IGenericRepository<RestaurantCategory> RestaurantCategories { get; }
        public IGenericRepository<Dish> Dishes { get; }
        public IGenericRepository<Review> Reviews { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Users = new GenericRepository<User>(_context);
            Restaurants = new GenericRepository<Restaurant>(_context);
            RestaurantCategories = new GenericRepository<RestaurantCategory>(_context);
            Dishes = new GenericRepository<Dish>(_context);
            Reviews = new GenericRepository<Review>(_context);
        }

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
    }

}
