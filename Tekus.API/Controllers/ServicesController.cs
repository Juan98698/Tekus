using Microsoft.AspNetCore.Mvc;
using Tekus.Application.Common;
using Tekus.Application.UseCases.Services;

namespace Tekus.API.Controllers
{
    [ApiController]
    [Route("api/services")]
    public class ServicesController : ControllerBase
    {
        private readonly ListServicesPagedUseCase _useCase;

        public ServicesController(ListServicesPagedUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PagedRequest request)
        {
            var result = await _useCase.ExecuteAsync(request);
            return Ok(result);
        }
    }
}
