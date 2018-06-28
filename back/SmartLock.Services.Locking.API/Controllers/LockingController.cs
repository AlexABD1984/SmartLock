using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CacheManager.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SmartLock.Services.Locking.API.Controllers
{
    [Route("api/v1/[controller]/{id:Guid}")]
    [ApiController]
    public class LockingController : ControllerBase
    {
        private readonly ILogger<LockingController> _logger=null;
        private readonly ICacheManager<bool> _cache = null;

        public LockingController(ILogger<LockingController> logger, ICacheManager<bool> cache/*, CatalogContext context*/)
        {
            _logger = logger;
            _cache = cache;
            //((DbContext)context).ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        // GET api/v1/[controller]/items[?pageSize=3&pageIndex=10]
        [HttpPost]
        [Route("Lock")]
        //[ProducesResponseType(typeof(PaginatedItemsViewModel<CatalogItem>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(IEnumerable<CatalogItem>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(CatalogItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Lock([FromQuery]string OTP, [FromQuery]Guid userId, [FromQuery] string id = null)
        {
            var result = new LockingResult();
            bool r = false;

            try
            {
                _logger.LogInformation($"User {"Ali"} request {"Open"} Lock with Id={id}");
                if (UserHasAccess(userId))
                {
                    r = await OpenLock(id);                       
                }

                if (r)
                {
                    _logger.LogInformation($"Request {"Open"} for Lock with Id={id} has been successful.");
                    //RegisterEvent();
                }
                else
                    _logger.LogWarning($"Request {"Open"} for Lock with Id={id} has been faild.");
                                
            }
            catch (Exception ex)
            {

            }
            return Ok(result);
        }

        private bool UserHasAccess(Guid userId)
        {
            if (_cache.Exists(userId.ToString()))
            {
                bool HasAccess = _cache.Get(userId.ToString());
                if (HasAccess)
                    return true;
            }
            return false;
        }

        private Task<bool> OpenLock(string id)
        {
            return Task.FromResult<bool>(true);
        }

        public enum LockCommand
        {
            Open,
            Lock
        }

    }
}