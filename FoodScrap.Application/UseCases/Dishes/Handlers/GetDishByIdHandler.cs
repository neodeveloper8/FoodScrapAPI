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
    public class GetDishByIdHandler : IRequestHandler<GetDishByIdQuery, DishResponseDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDishByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DishResponseDto?> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
        {
            var dish = await _unitOfWork.Dishes
                .FindAsync(d => d.Id == request.Id, q => q.Include(d => d.Restaurant));

            var entity = dish.FirstOrDefault();
            if (entity == null) return null;

            return new DishResponseDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                Type = entity.Type,
                RestaurantId = entity.RestaurantId,
                RestaurantName = entity.Restaurant?.Name ?? "(Sin restaurante)"
            };
        }
    }
}
