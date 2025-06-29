using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodScrap.Application.UseCases.Restaurants.Commands;
using FoodScrap.Domain.Entities;
using FoodScrap.Domain.Interfaces;
using MediatR;

namespace FoodScrap.Application.UseCases.Restaurants.Handlers
{
    public class CreateRestaurantHandler : IRequestHandler<CreateRestaurantCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateRestaurantHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var entity = new Restaurant
            {
                Id = Guid.NewGuid(),
                Name = request.Restaurant.Name,
                Address = request.Restaurant.Address,
                Latitude = request.Restaurant.Latitude,
                Longitude = request.Restaurant.Longitude,
                CategoryId = request.Restaurant.CategoryId
            };

            await _unitOfWork.Restaurants.AddAsync(entity);
            await _unitOfWork.CompleteAsync();

            return entity.Id;
        }
    }
}
