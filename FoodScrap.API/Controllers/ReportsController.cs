using FoodScrap.Application.UseCases.Reports.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodScrap.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("dishes")]
        public async Task<IActionResult> GetDishReport()
        {
            var file = await _mediator.Send(new GetDishReportQuery());

            return File(file,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "reporte-platos.xlsx");
        }
    }
}
