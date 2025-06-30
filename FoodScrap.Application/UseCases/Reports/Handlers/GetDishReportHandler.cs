using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodScrap.Application.UseCases.Reports.Queries;
using FoodScrap.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FoodScrap.Infrastructure.Reports;


namespace FoodScrap.Application.UseCases.Reports.Handlers
{
    public class GetDishReportHandler : IRequestHandler<GetDishReportQuery, byte[]>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ExcelReportService _reportService;

        public GetDishReportHandler(IUnitOfWork unitOfWork, ExcelReportService reportService)
        {
            _unitOfWork = unitOfWork;
            _reportService = reportService;
        }

        public async Task<byte[]> Handle(GetDishReportQuery request, CancellationToken cancellationToken)
        {
            var dishes = await _unitOfWork.Dishes.FindAsync(
                predicate: null,
                include: q => q.Include(d => d.Restaurant).Include(d => d.Reviews)
            );

            return _reportService.GenerateDishReport(dishes.ToList());
        }
    }
}
