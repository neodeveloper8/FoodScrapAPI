using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodScrap.Application.DTOs;
using FoodScrap.Application.UseCases.Stats.Queries;
using FoodScrap.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodScrap.Application.UseCases.Stats.Handlers
{
    public class GetTopReviewedRestaurantsHandler : IRequestHandler<GetTopReviewedRestaurantsQuery, List<TopReviewedRestaurantDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTopReviewedRestaurantsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<TopReviewedRestaurantDto>> Handle(GetTopReviewedRestaurantsQuery request, CancellationToken cancellationToken)
        {
            var restaurants = await _unitOfWork.Restaurants.FindAsync(
                predicate: null,
                include: q => q.Include(r => r.Dishes).ThenInclude(d => d.Reviews)
            );

            return restaurants
                .Select(r => new TopReviewedRestaurantDto
                {
                    RestaurantName = r.Name,
                    TotalReviews = r.Dishes.SelectMany(d => d.Reviews).Count()
                })
                .OrderByDescending(r => r.TotalReviews)
                .Take(5)
                .ToList();
        }
    }
}
