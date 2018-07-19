using CacheManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartLock.Services.AccessRight.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLock.Services.AccessRight.API.ApplicationServices
{
    public class AccessRightService : IAccessRightService
    {
        private readonly ILogger<AccessRightService> _logger = null;
        private readonly ICacheManager<bool> _cache = null;
        private readonly AccessRightDbContext _dbContext = null;

        public AccessRightService(ILogger<AccessRightService> logger, ICacheManager<bool> cache, AccessRightDbContext dbContext)
        {
            _cache = cache;
            _logger = logger;
            _dbContext = dbContext;
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
            var response = await CheckAccessRightbyDataBase(userId, lockId);
            return response;
        }

        private async Task<bool> CheckAccessRightbyDataBase(Guid userId, Guid lockId)
        {
            try
            {
                var accessRight = await _dbContext.AccessRights.FirstOrDefaultAsync(c => c.UserId == userId && c.LockId == lockId);

                if (accessRight != null)
                {
                    string cacheKey = $"UserId={userId}&LockId={lockId}";
                    _cache.Add(cacheKey, accessRight.HasAccess);
                    return accessRight.HasAccess;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in  Access Check");
            }
            return false;
        }
    }
}
