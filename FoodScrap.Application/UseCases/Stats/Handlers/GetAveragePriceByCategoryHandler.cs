using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodScrap.Application.DTOs;
using FoodScrap.Application.UseCases.Stats.Queries;
using FoodScrap.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodScrap.Application.UseCases.Stats.Handlers
{
    public class GetAveragePriceByCategoryHandler : IRequestHandler<GetAveragePriceByCategoryQuery, List<AveragePriceByCategoryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAveragePriceByCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<AveragePriceByCategoryDto>> Handle(GetAveragePriceByCategoryQuery request, CancellationToken cancellationToken)
        {
            var dishes = await _unitOfWork.Dishes.FindAsync(
                predicate: null,
                include: q => q.Include(d => d.Restaurant).ThenInclude(r => r.Category)
            );

            return dishes
                .Where(d => d.Restaurant?.Category != null)
                .GroupBy(d => d.Restaurant!.Category!.Name)
                .Select(g => new AveragePriceByCategoryDto
                {
                    CategoryName = g.Key,
                    AveragePrice = Math.Round(g.Average(d => (double)d.Price), 2)
                })
                .ToList();
        }
    }
}
