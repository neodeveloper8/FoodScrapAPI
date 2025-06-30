using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodScrap.Application.DTOs;
using MediatR;

namespace FoodScrap.Application.UseCases.Dishes.Commands
{
    public record CreateDishCommand(DishDto Dish) : IRequest<Guid>;
}
