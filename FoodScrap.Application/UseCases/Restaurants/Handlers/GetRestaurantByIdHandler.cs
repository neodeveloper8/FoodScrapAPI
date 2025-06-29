using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodScrap.Application.DTOs;
using FoodScrap.Application.UseCases.Restaurants.Queries;
using FoodScrap.Domain.Interfaces;
using MediatR;

namespace FoodScrap.Application.UseCases.Restaurants.Handlers
{
    public class GetRestaurantByIdHandler : IRequestHandler<GetRestaurantByIdQuery, RestaurantResponseDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRestaurantByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RestaurantResponseDto?> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Restaurants.GetByIdWithCategoryAsync(request.Id);
            if (entity == null) return null;

            return new RestaurantResponseDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Address = entity.Address,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude,
                CategoryName = entity.Category?.Name ?? ""
            };
        }
    }
}
