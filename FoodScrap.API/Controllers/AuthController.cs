using FoodScrap.Application.DTOs;
using FoodScrap.Application.UseCases.Auth.Commands;
using FoodScrap.Application.UseCases.Auth.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodScrap.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto user)
        {
            var result = await _mediator.Send(new RegisterUserCommand(user));
            if (!result) return BadRequest("El usuario ya existe");
            return Ok("Usuario registrado correctamente");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var token = await _mediator.Send(new LoginUserCommand(login));
            if (token is null) return Unauthorized("Credenciales inválidas");
            return Ok(new { token });
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> Me()
        {
            var result = await _mediator.Send(new GetCurrentUserQuery());
            return Ok(result);
        }

    }
}
