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
    public class DeleteDishHandler : IRequestHandler<DeleteDishCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDishHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteDishCommand request, CancellationToken cancellationToken)
        {
            var dish = await _unitOfWork.Dishes.GetByIdAsync(request.Id);
            if (dish == null) return false;

            _unitOfWork.Dishes.Remove(dish);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
