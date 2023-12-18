using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Batsy.Resources.Services
{
    public sealed class CandidateService
    {
        private readonly HttpClient _httpClient;
        public CandidateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
