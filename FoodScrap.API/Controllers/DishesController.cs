using FoodScrap.Application.DTOs;
using FoodScrap.Application.UseCases.Dishes.Commands;
using FoodScrap.Application.UseCases.Dishes.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodScrap.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DishesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DishesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(DishDto dto)
        {
            var id = await _mediator.Send(new CreateDishCommand(dto));
            return Ok(new { id });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllDishesQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetDishByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(Guid id, DishDto dto)
        {
            var success = await _mediator.Send(new UpdateDishCommand(id, dto));
            if (!success) return NotFound();
            return NoContent();
        }


    }
}
