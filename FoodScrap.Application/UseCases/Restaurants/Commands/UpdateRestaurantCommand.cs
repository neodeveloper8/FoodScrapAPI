using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodScrap.Application.DTOs;
using MediatR;

namespace FoodScrap.Application.UseCases.Restaurants.Commands
{
    public record UpdateRestaurantCommand(Guid Id, RestaurantDto Restaurant) : IRequest<bool>;
}
