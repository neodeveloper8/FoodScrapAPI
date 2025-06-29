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
    public class GetAllRestaurantsHandler : IRequestHandler<GetAllRestaurantsQuery, List<RestaurantResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllRestaurantsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<RestaurantResponseDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            var restaurants = await _unitOfWork.Restaurants.GetAllWithCategoryAsync();


            // Incluye la categoría de cada restaurante
            var data = restaurants
                .Select(r => new RestaurantResponseDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Address = r.Address,
                    Latitude = r.Latitude,
                    Longitude = r.Longitude,
                    CategoryName = r.Category?.Name ?? ""
                })
                .ToList();

            return data;
        }
    }
}
