using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tekus.Application.UseCases.Reports;

namespace Tekus.API.Controllers
{
    [ApiController]
    [Route("api/reports")]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly GetSummaryReportUseCase _useCase;

        public ReportsController(GetSummaryReportUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            var result = await _useCase.ExecuteAsync();
            return Ok(result);
        }
    }
}
