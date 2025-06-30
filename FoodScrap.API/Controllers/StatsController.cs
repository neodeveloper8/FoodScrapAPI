using FoodScrap.Application.UseCases.Stats.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodScrap.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StatsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("top-rated-dishes")]
        public async Task<IActionResult> GetTopRatedDishes()
        {
            var result = await _mediator.Send(new GetTopRatedDishesQuery());
            return Ok(result);
        }

        [HttpGet("top-reviewed-restaurants")]
        public async Task<IActionResult> GetTopReviewedRestaurants()
        {
            var result = await _mediator.Send(new GetTopReviewedRestaurantsQuery());
            return Ok(result);
        }

        [HttpGet("avg-price-by-category")]
        public async Task<IActionResult> GetAveragePriceByCategory()
        {
            var result = await _mediator.Send(new GetAveragePriceByCategoryQuery());
            return Ok(result);
        }
    }
}
