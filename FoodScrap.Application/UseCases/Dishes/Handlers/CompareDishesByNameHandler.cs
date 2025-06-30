using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodScrap.Application.DTOs;
using FoodScrap.Application.UseCases.Dishes.Queries;
using FoodScrap.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodScrap.Application.UseCases.Dishes.Handlers
{
    public class CompareDishesByNameHandler : IRequestHandler<CompareDishesByNameQuery, List<ComparableDishDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompareDishesByNameHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ComparableDishDto>> Handle(CompareDishesByNameQuery request, CancellationToken cancellationToken)
        {
            var dishes = await _unitOfWork.Dishes.FindAsync(
                d => d.Name.ToLower().Contains(request.Name.ToLower()),
                include: q => q
                    .Include(d => d.Restaurant)
                        .ThenInclude(r => r.Category)
                    .Include(d => d.Reviews)
            );

            var result = dishes.Select(d => new ComparableDishDto
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                Price = d.Price,
                Type = d.Type,
                RestaurantName = d.Restaurant?.Name ?? "(Sin restaurante)",
                CategoryName = d.Restaurant?.Category?.Name ?? "(Sin categoría)",
                AverageRating = d.Reviews?.Any() == true ? d.Reviews.Average(r => r.Rating ?? 0) : 0
            }).ToList();

            return result;
        }
    }
}
