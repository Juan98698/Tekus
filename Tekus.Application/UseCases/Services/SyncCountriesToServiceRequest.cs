using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Application.UseCases.Services
{
    public class SyncCountriesToServiceRequest
    {
        public Guid ProviderId { get; set; }
        public Guid ServiceId { get; set; }
        public List<string> CountryCodes { get; set; } = new();
    }
}
