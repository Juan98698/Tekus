using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Tekus.Frontend;
using Tekus.Frontend.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri("https://localhost:7001/")
    });
builder.Services.AddMudServices();
builder.Services.AddScoped<CountryApiService>();
builder.Services.AddScoped<ProviderApiService>();
builder.Services.AddScoped<ServiceApiService>();
builder.Services.AddScoped<SummaryApiService>();
await builder.Build().RunAsync();
