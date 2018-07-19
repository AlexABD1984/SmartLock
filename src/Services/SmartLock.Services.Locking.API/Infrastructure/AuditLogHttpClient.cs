using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmartLock.Services.AuditLogs.API.Model;
using SmartLock.Services.Locking.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SmartLock.Services.Locking.API.Infrastructure
{
    public class AuditLogHttpClient
    {
        private readonly ILogger<AuditLogHttpClient> _logger;
        public AuditLogHttpClient(HttpClient httpClient, ILogger<AuditLogHttpClient> logger, IConfiguration config)
        {
            HttpClient = httpClient;
            _logger = logger;
        }

        public HttpClient HttpClient { get; }

        public async Task<Guid> CallAuditLog(Guid lockId, Guid userId, LockCommand command)
        {
            try
            {
                var AddAuditLogServiceUri = new Uri($"/api/v1/AuditLogs/PostAuditLog", UriKind.Relative);
                var auditLog = new AuditLog(userId, lockId, command.ToString());

                var response = await HttpClient.PostAsJsonAsync<AuditLog>(AddAuditLogServiceUri, auditLog);
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<Guid>();
                }
                else
                    return Guid.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in  CallAuditLog", ex.Message);
            }
            return Guid.Empty;
        }

        public async Task<Guid> UpdateAuditLog(Guid auditLogId, OpenLockResult result)
        {
            try
            {
                var UpdateAuditLogServiceUri = new Uri($"/api/v1/AuditLogs/{auditLogId}/{result}", UriKind.Relative);
                //var auditLog = new AuditLog(auditLogId, result.ToString());

                var response = await HttpClient.PutAsync(UpdateAuditLogServiceUri, null);
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<Guid>();
                }
                else
                    return Guid.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in  UpdateAuditLog", ex.Message);
            }
            return Guid.Empty;
        }
    }
}
