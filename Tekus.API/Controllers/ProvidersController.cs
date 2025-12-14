using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tekus.API.Security;
using Tekus.Application.Common;
using Tekus.Application.DTOs;
using Tekus.Application.UseCases.Providers;


namespace Tekus.API.Controllers
{
    [ApiController]
    [Route("api/providers")]
    //[Authorize]
    public class ProvidersController : ControllerBase
    {
        private readonly CreateProviderUseCase _createProviderUseCase;
        private readonly ListProvidersUseCase _listProvidersUseCase;
        private readonly AddServiceToProviderUseCase _addServiceToProviderUseCase;

        public ProvidersController(
            CreateProviderUseCase createProviderUseCase,
            ListProvidersUseCase listProvidersUseCase,
            AddServiceToProviderUseCase addServiceToProviderUseCase)
        {
            _createProviderUseCase = createProviderUseCase;
            _listProvidersUseCase = listProvidersUseCase;
            _addServiceToProviderUseCase = addServiceToProviderUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProviderRequest request)
        {
            var result = await _createProviderUseCase.ExecuteAsync(request);
            return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetProviders([FromQuery] PagedRequest request)
        {
            var result = await _listProvidersUseCase.ExecuteAsync(request);
            return Ok(result);
        }

        [HttpPost("{id:guid}/services")]
        public async Task<IActionResult> AddService(
            Guid id,
            [FromBody] AddServiceRequest request)
        {
            request.ProviderId = id;
            await _addServiceToProviderUseCase.ExecuteAsync(request);
            return NoContent();
        }
    }
}
