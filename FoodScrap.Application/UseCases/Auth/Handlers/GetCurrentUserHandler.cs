using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodScrap.Application.DTOs;
using FoodScrap.Application.UseCases.Auth.Queries;
using FoodScrap.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FoodScrap.Application.UseCases.Auth.Handlers
{
    public class GetCurrentUserHandler : IRequestHandler<GetCurrentUserQuery, UserProfileDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public GetCurrentUserHandler(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserProfileDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst("id");

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
                throw new UnauthorizedAccessException("No se pudo obtener el ID del usuario autenticado.");

            var user = await _unitOfWork.Users.GetByIdAsync(userId);

            if (user == null)
                throw new Exception("Usuario no encontrado.");

            return new UserProfileDto
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name
            };
        }
    }
}
