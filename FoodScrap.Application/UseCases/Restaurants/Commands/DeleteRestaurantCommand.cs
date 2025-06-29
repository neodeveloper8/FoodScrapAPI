using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FoodScrap.Application.UseCases.Restaurants.Commands
{
    public record DeleteRestaurantCommand(Guid Id) : IRequest<bool>;
}
