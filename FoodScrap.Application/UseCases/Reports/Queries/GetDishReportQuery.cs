using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FoodScrap.Application.UseCases.Reports.Queries
{
    public record GetDishReportQuery : IRequest<byte[]>;
}
