using FoodScrap.Application.UseCases.Reviews.Commands;
using FoodScrap.Application.UseCases.Reviews.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodScrap.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReviewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("by-dish/{dishId}")]
        public async Task<IActionResult> GetByDish(Guid dishId)
        {
            var result = await _mediator.Send(new GetReviewsByDishIdQuery(dishId));
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateReviewCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new { reviewId = result });
        }
    }
}
