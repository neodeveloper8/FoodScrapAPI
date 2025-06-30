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
    public class GetFilteredDishesHandler : IRequestHandler<GetFilteredDishesQuery, List<DishResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFilteredDishesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<DishResponseDto>> Handle(GetFilteredDishesQuery request, CancellationToken cancellationToken)
        {
            var filter = request.Filter;

            // Incluimos la relación con Restaurant (para acceder a CategoryId y Name)
            var dishes = await _unitOfWork.Dishes.FindAsync(
                predicate: null,
                include: q => q.Include(d => d.Restaurant)
            );

            // Aplicamos el filtro ya con la relación cargada
            var filtered = dishes.Where(d =>
                (string.IsNullOrEmpty(filter.Name) || d.Name.Contains(filter.Name)) &&
                (string.IsNullOrEmpty(filter.Type) || d.Type == filter.Type) &&
                (!filter.RestaurantId.HasValue || d.RestaurantId == filter.RestaurantId) &&
                (!filter.MinPrice.HasValue || d.Price >= filter.MinPrice.Value) &&
                (!filter.MaxPrice.HasValue || d.Price <= filter.MaxPrice.Value) &&
                (!filter.CategoryId.HasValue || d.Restaurant?.CategoryId == filter.CategoryId)
            );

            var result = filtered.Select(d => new DishResponseDto
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                Price = d.Price,
                Type = d.Type,
                RestaurantId = d.RestaurantId,
                RestaurantName = d.Restaurant?.Name ?? "(Sin restaurante)"
            });

            return filter.OrderByPriceAsc
                ? result.OrderBy(d => d.Price).ToList()
                : result.OrderByDescending(d => d.Price).ToList();
        }
    }
}
