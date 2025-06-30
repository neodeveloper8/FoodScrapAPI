using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodScrap.Application.DTOs;
using FoodScrap.Application.UseCases.Restaurants.Queries;
using FoodScrap.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodScrap.Application.UseCases.Restaurants.Handlers
{
    public class GetRestaurantsByCategoryHandler : IRequestHandler<GetRestaurantsByCategoryQuery, List<RestaurantResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRestaurantsByCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<RestaurantResponseDto>> Handle(GetRestaurantsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var restaurants = await _unitOfWork.Restaurants.FindAsync(r => r.CategoryId == request.CategoryId,
                include: q => q.Include(r => r.Category));

            return restaurants.Select(r => new RestaurantResponseDto
            {
                Id = r.Id,
                Name = r.Name,
                Address = r.Address,
                Latitude = r.Latitude,
                Longitude = r.Longitude,
                CategoryName = r.Category?.Name ?? ""
            }).ToList();
        }
    }
}
