using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodScrap.Application.UseCases.Restaurants.Commands;
using FoodScrap.Domain.Interfaces;
using MediatR;

namespace FoodScrap.Application.UseCases.Restaurants.Handlers
{
    public class UpdateRestaurantHandler : IRequestHandler<UpdateRestaurantCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRestaurantHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await _unitOfWork.Restaurants.GetByIdAsync(request.Id);
            if (restaurant == null) return false;

            restaurant.Name = request.Restaurant.Name;
            restaurant.Address = request.Restaurant.Address;
            restaurant.Latitude = request.Restaurant.Latitude;
            restaurant.Longitude = request.Restaurant.Longitude;
            restaurant.CategoryId = request.Restaurant.CategoryId;

            _unitOfWork.Restaurants.Update(restaurant);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
