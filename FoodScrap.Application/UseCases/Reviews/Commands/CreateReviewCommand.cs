using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FoodScrap.Application.UseCases.Reviews.Commands
{
    public record CreateReviewCommand(
    Guid DishId,
    
    string Comment,
    int Rating
) : IRequest<Guid>;
}
