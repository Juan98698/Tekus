using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tekus.API.Security;
using Tekus.Application.Common;
using Tekus.Application.DTOs;
using Tekus.Application.UseCases.Providers;
using Tekus.Application.UseCases.Services;


namespace Tekus.API.Controllers
{
    [ApiController]
    [Route("api/providers")]

    public class ProvidersController : ControllerBase
    {
        private readonly CreateProviderUseCase _createProviderUseCase;
        private readonly ListProvidersUseCase _listProvidersUseCase;
        private readonly AddServiceToProviderUseCase _addServiceToProviderUseCase;
        private readonly UpdateProviderUseCase _updateProviderUseCase;
        private readonly DeleteProviderUseCase _deleteProviderUseCase;
        private readonly UpdateServiceUseCase _updateServiceUseCase;
        private readonly DeleteServiceUseCase _deleteServiceUseCase;
        private readonly ListServicesByProviderPagedUseCase _listServicesByProviderPagedUseCase;
        private readonly AssignCountriesToServiceUseCase _assignCountriesUseCase;


        public ProvidersController(
            CreateProviderUseCase createProviderUseCase,
            ListProvidersUseCase listProvidersUseCase,
            AddServiceToProviderUseCase addServiceToProviderUseCase,
            UpdateProviderUseCase updateProviderUseCase,
            DeleteProviderUseCase deleteProviderUseCase,
            UpdateServiceUseCase updateServiceUseCase,
            DeleteServiceUseCase deleteServiceUseCase,
            ListServicesByProviderPagedUseCase listServicesByProviderPagedUseCase,
            AssignCountriesToServiceUseCase assignCountriesUseCase
            )
            
        {
            _createProviderUseCase = createProviderUseCase;
            _listProvidersUseCase = listProvidersUseCase;
            _addServiceToProviderUseCase = addServiceToProviderUseCase;
            _updateProviderUseCase = updateProviderUseCase;
            _deleteProviderUseCase = deleteProviderUseCase;
            _updateServiceUseCase = updateServiceUseCase;
            _deleteServiceUseCase = deleteServiceUseCase;
            _listServicesByProviderPagedUseCase = listServicesByProviderPagedUseCase;
            _assignCountriesUseCase = assignCountriesUseCase;



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

            var result = await _addServiceToProviderUseCase.ExecuteAsync(request);

            return Ok(result); 
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateProviderRequest request)
        {
            request.Id = id;
            await _updateProviderUseCase.ExecuteAsync(request);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _deleteProviderUseCase.ExecuteAsync(id);
            return NoContent();
        }
        //Endpoints de servicios por proveedor
  

        [HttpPut("{providerId:guid}/services/{serviceId:guid}")]
        public async Task<IActionResult> UpdateService(
        Guid providerId,
        Guid serviceId,
        UpdateServiceRequest request)
        {
            request.ProviderId = providerId;
            request.ServiceId = serviceId;

            await _updateServiceUseCase.ExecuteAsync(request);
            return NoContent();
        }

        [HttpDelete("{providerId:guid}/services/{serviceId:guid}")]
        public async Task<IActionResult> DeleteService(Guid providerId, Guid serviceId)
        {
            await _deleteServiceUseCase.ExecuteAsync(providerId, serviceId);
            return NoContent();
        }

        [HttpGet("{providerId:guid}/services")]
        public async Task<IActionResult> GetServices(
        Guid providerId,
        [FromQuery] PagedRequest request)
        {
            var result =
                await _listServicesByProviderPagedUseCase.ExecuteAsync(providerId, request);

            return Ok(result);
        }

        //Endpints con Country

        [HttpPost("{providerId:guid}/services/{serviceId:guid}/countries")]
        public async Task<IActionResult> AssignCountries(
        Guid providerId,
        Guid serviceId,
        [FromBody] List<string> countryCodes)
        {
            await _assignCountriesUseCase.ExecuteAsync(new AssignCountriesToServiceRequest
            {
                ProviderId = providerId,
                ServiceId = serviceId,
                CountryCodes = countryCodes
            });

            return NoContent();
        }
    }
}
