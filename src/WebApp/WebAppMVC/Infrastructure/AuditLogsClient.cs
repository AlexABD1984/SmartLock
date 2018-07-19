using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmartLock.ClientApp.MVC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebAppMVC.Model;

namespace WebAppMVC.Infrastructure
{
    public class AuditLogClient
    {
        private HttpClient _client;
        private ILogger<AuditLogClient> _logger;
        private readonly string _apiKey;

        public AuditLogClient(HttpClient client, ILogger<AuditLogClient> logger, IConfiguration config)
        {
            _client = client;
            //_client.BaseAddress = new Uri($"http://localhost:5002/api/v1/AuditLogs/"); //Could also be set in Startup.cs
            _logger = logger;
            _apiKey = config[""];
        }

        public async Task<PaginatedList<AuditLog>> GetAuditLogsAsync(int pageIndex,int pageSize)
        {
            try
            {
                var auditLogApiUri = new Uri($"/api/v1/AuditLogs/?PageSize={pageSize}&PageIndex={pageIndex}", UriKind.Relative);
                var response = await _client.GetAsync(auditLogApiUri);
                response.EnsureSuccessStatusCode();
                string jsonResult = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<PaginatedList<AuditLog>>(jsonResult);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An error occurred connecting to AuditLog Service API {ex.ToString()}");
                throw;
            }
        }
    }
}
