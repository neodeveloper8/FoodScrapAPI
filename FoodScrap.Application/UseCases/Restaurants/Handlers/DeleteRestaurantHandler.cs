using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodScrap.Application.UseCases.Restaurants.Commands;
using FoodScrap.Domain.Interfaces;
using MediatR;

namespace FoodScrap.Application.UseCases.Restaurants.Handlers
{
    public class DeleteRestaurantHandler : IRequestHandler<DeleteRestaurantCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRestaurantHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await _unitOfWork.Restaurants.GetByIdAsync(request.Id);
            if (restaurant == null) return false;

            _unitOfWork.Restaurants.Remove(restaurant);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
