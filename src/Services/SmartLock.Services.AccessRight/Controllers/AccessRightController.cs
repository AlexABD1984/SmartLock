using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartLock.Services.AccessRight.API.ApplicationServices;
using SmartLock.Services.AccessRight.Data;

namespace SmartLock.Services.AccessRight.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccessRightController : ControllerBase
    {
        private readonly ILogger<AccessRightController> _logger = null;
        private readonly IAccessRightService _accessRightService = null;

        public AccessRightController(ILogger<AccessRightController> logger, IAccessRightService accessRightService)
        {
            _logger = logger;
            _accessRightService = accessRightService;
        }

        // GET api/v1/[controller]/hasaccess[?userId=00000000-0000-0000-0000-000000000000&objectId=00000000-0000-0000-0000-000000000000]
        [HttpGet]
        [Route("hasaccess")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<bool> HasAccess([FromQuery]Guid userId, [FromQuery]Guid lockId)
        {
            bool result = false;
            try
            {
                if (userId != Guid.Empty && lockId != Guid.Empty)
                    result = await _accessRightService.HasAccess(userId, lockId);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, null);
            }
            return result;
        }
    }
}