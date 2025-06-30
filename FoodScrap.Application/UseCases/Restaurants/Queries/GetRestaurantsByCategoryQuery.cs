using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodScrap.Application.DTOs;
using MediatR;

namespace FoodScrap.Application.UseCases.Restaurants.Queries
{
    public record GetRestaurantsByCategoryQuery(Guid CategoryId) : IRequest<List<RestaurantResponseDto>>;
}
