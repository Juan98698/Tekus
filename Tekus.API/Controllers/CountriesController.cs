using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tekus.Application.Interfaces.Services;

namespace Tekus.API.Controllers;

[ApiController]
[Route("api/countries")]
[Authorize]
public class CountriesController : ControllerBase
{
    private readonly ICountryProvider _countryProvider;

    public CountriesController(ICountryProvider countryProvider)
    {
        _countryProvider = countryProvider;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var countries = await _countryProvider.GetAllAsync();

        return Ok(countries.Select(c => new
        {
            code = c.Code,
            name = c.Name
        }));
    }
}

