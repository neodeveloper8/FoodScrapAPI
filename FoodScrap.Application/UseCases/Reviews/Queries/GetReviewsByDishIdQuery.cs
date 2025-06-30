using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodScrap.Application.DTOs;
using MediatR;

namespace FoodScrap.Application.UseCases.Reviews.Queries
{
    public record GetReviewsByDishIdQuery(Guid DishId) : IRequest<List<ReviewResponseDto>>;
}
