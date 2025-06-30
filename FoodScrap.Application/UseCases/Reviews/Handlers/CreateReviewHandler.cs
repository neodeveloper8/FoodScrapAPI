using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodScrap.Application.UseCases.Reviews.Commands;
using FoodScrap.Domain.Entities;
using FoodScrap.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;



namespace FoodScrap.Application.UseCases.Reviews.Handlers
{
    public class CreateReviewHandler : IRequestHandler<CreateReviewCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateReviewHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Guid> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst("id");

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
                throw new UnauthorizedAccessException("No se pudo obtener el ID del usuario autenticado.");

            var newReview = new Review
            {
                Id = Guid.NewGuid(),
                DishId = request.DishId,
                UserId = userId,
                Comment = request.Comment,
                Rating = request.Rating,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Reviews.AddAsync(newReview);
            await _unitOfWork.CompleteAsync();

            return newReview.Id;
        }
    }
}
