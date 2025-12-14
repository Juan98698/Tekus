using Tekus.API.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tekus.Application.DTOs;
using Tekus.Application.UseCases.Providers;


namespace Tekus.API.Controllers
{

    [ApiController]
    [Route("api/providers")]
    [Authorize]
    public class ProvidersController : Controller
    {
        private readonly CreateProviderUseCase _createProvider;

        public ProvidersController(CreateProviderUseCase createProvider)
        {
            _createProvider = createProvider;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProviderRequest request)
        {
            var result = await _createProvider.ExecuteAsync(request);
            return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
        }
    }
}
