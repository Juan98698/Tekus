using System.Net.Http;
using System.Text.Json;

namespace Tekus.Frontend.Helpers
{
    public static class HttpErrorHelper
    {
        public static async Task<string> GetErrorMessageAsync(HttpResponseMessage response)
        {
            try
            {
                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);

                if (doc.RootElement.TryGetProperty("error", out var error))
                    return error.GetString() ?? "Error desconocido";
            }
            catch { }

            return "Ocurrió un error inesperado";
        }
    }
}
