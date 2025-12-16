using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tekus.Application.UseCases.Summary;

namespace Tekus.API.Controllers
{
    [ApiController]
    [Route("api/summary")]
    [Authorize]
    public class SummaryController : ControllerBase
    {
        private readonly GetSummaryUseCase _useCase;

        public SummaryController(GetSummaryUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _useCase.ExecuteAsync();
            return Ok(result);
        }
    }
}
