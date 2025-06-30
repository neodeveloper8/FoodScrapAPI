using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodScrap.Application.UseCases.Dishes.Commands;
using FoodScrap.Domain.Interfaces;
using MediatR;

namespace FoodScrap.Application.UseCases.Dishes.Handlers
{
    public class UpdateDishHandler : IRequestHandler<UpdateDishCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDishHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateDishCommand request, CancellationToken cancellationToken)
        {
            var dish = await _unitOfWork.Dishes.GetByIdAsync(request.Id);
            if (dish == null) return false;

            dish.Name = request.Dish.Name;
            dish.Description = request.Dish.Description;
            dish.Price = request.Dish.Price;
            dish.Type = request.Dish.Type;
            dish.RestaurantId = request.Dish.RestaurantId;

            _unitOfWork.Dishes.Update(dish);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
