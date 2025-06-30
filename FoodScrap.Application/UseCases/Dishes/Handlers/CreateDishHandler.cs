using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodScrap.Application.UseCases.Dishes.Commands;
using FoodScrap.Domain.Entities;
using FoodScrap.Domain.Interfaces;
using MediatR;

namespace FoodScrap.Application.UseCases.Dishes.Handlers
{
    public class CreateDishHandler : IRequestHandler<CreateDishCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateDishHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            var entity = new Dish
            {
                Id = Guid.NewGuid(),
                Name = request.Dish.Name,
                Description = request.Dish.Description,
                Price = request.Dish.Price,
                Type = request.Dish.Type,
                RestaurantId = request.Dish.RestaurantId
            };

            await _unitOfWork.Dishes.AddAsync(entity);
            await _unitOfWork.CompleteAsync();

            return entity.Id;
        }
    }
}
