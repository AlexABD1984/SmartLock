using CacheManager.Core;
using Microsoft.Extensions.Logging;
using SmartLock.Services.Locking.API.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SmartLock.Services.Locking.API.ApplicationServices
{
    public class UserAccessService : IUserAccessService
    {
        private readonly ILogger<UserAccessService> _logger = null;
        private readonly ICacheManager<bool> _cache = null;
        private readonly AccessRightHttpClient _accessRightHttpClient = null;
        public UserAccessService(ILogger<UserAccessService> logger,ICacheManager<bool> cache, AccessRightHttpClient accessRightHttpClient)
        {
            _cache = cache;
            _logger = logger;
            _accessRightHttpClient = accessRightHttpClient;
        }
        public async Task<bool> HasAccess(Guid userId, Guid lockId)
        {
            string cacheKey = $"UserId={userId}&LockId={lockId}";
            if (_cache.Exists(cacheKey))
            {
                bool HasAccess = _cache.Get(cacheKey);
                if (HasAccess)
                    return true;
            }
            var response = await CheckUserbyService(userId, lockId);
            return response;
        }

        private async Task<bool> CheckUserbyService(Guid userId, Guid lockId)
        {
            try
            {   
                var hasAccessServiceUri = $"hasaccess/?userid={userId}&lockid={lockId}";
                var response = await _accessRightHttpClient.Client.GetAsync(new Uri(hasAccessServiceUri, UriKind.Relative));//.ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<bool>().Result;
                }
                else
                    return false;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,"Error in  Access Check");
            }
            return false;
        }
    }
}
