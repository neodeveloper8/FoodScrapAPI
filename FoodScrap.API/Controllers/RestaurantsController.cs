using FoodScrap.Application.DTOs;
using FoodScrap.Application.UseCases.Restaurants.Commands;
using FoodScrap.Application.UseCases.Restaurants.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodScrap.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RestaurantsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(RestaurantDto dto)
        {
            var id = await _mediator.Send(new CreateRestaurantCommand(dto));
            return Ok(new { id });
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllRestaurantsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetRestaurantByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(Guid id, RestaurantDto dto)
        {
            var success = await _mediator.Send(new UpdateRestaurantCommand(id, dto));
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _mediator.Send(new DeleteRestaurantCommand(id));
            if (!success) return NotFound();
            return NoContent();
        }




    }
}
