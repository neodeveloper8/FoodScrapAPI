using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodScrap.Application.DTOs;
using FoodScrap.Application.UseCases.Reviews.Queries;
using FoodScrap.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodScrap.Application.UseCases.Reviews.Handlers
{
    public class GetReviewsByDishIdHandler : IRequestHandler<GetReviewsByDishIdQuery, List<ReviewResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetReviewsByDishIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ReviewResponseDto>> Handle(GetReviewsByDishIdQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _unitOfWork.Reviews.FindAsync(
                r => r.DishId == request.DishId,
                include: q => q.Include(r => r.User)
            );

            return reviews.Select(r => new ReviewResponseDto
            {
                Id = r.Id,
                Comment = r.Comment,
                Rating = r.Rating ?? 0,
                CreatedAt = r.CreatedAt,
                UserName = r.User?.Name ?? "Anónimo"
            }).ToList();
        }
    }
}
