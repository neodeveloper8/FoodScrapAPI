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
    public class GetAllDishesHandler : IRequestHandler<GetAllDishesQuery, List<DishResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllDishesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<DishResponseDto>> Handle(GetAllDishesQuery request, CancellationToken cancellationToken)
        {
            var dishes = await _unitOfWork.Dishes
                .FindAsync(include: q => q.Include(d => d.Restaurant));

            return dishes.Select(d => new DishResponseDto
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                Price = d.Price,
                Type = d.Type,
                RestaurantId = d.RestaurantId,
                RestaurantName = d.Restaurant?.Name ?? "(Desconocido)"
            }).ToList();
        }
    }
}
