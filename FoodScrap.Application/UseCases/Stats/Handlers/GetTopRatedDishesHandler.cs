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
    public class GetTopRatedDishesHandler : IRequestHandler<GetTopRatedDishesQuery, List<TopRatedDishDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTopRatedDishesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<TopRatedDishDto>> Handle(GetTopRatedDishesQuery request, CancellationToken cancellationToken)
        {
            var dishes = await _unitOfWork.Dishes.FindAsync(
                predicate: null,
                include: q => q
                    .Include(d => d.Restaurant)
                    .Include(d => d.Reviews)
            );

            return dishes
                .Where(d => d.Reviews.Any())
                .Select(d => new TopRatedDishDto
                {
                    DishName = d.Name,
                    RestaurantName = d.Restaurant?.Name ?? "(Sin restaurante)",
                    AverageRating = d.Reviews.Average(r => r.Rating ?? 0)
                })
                .OrderByDescending(d => d.AverageRating)
                .Take(5)
                .ToList();
        }
    }
}
