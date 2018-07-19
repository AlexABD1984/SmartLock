using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CacheManager.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartLock.Services.Locking.API.ApplicationServices;
using SmartLock.Services.Locking.API.Model;
using SmartLock.Services.Locking.API.Model.ViewModel;

namespace SmartLock.Services.Locking.API.Controllers
{
    [Route("api/v1/[controller]/{lockId:Guid}")]
    [Authorize]
    [ApiController]
    public class LockingController : ControllerBase
    {
        private readonly ILogger<LockingController> _logger = null;
        private readonly IUserAccessService _userAccessService = null;
        private readonly ILockingService _lockingService = null;
        private readonly IAuditLogService _auditLogService = null;

        public LockingController(ILogger<LockingController> logger, IUserAccessService userAccessService,
            ILockingService lockingService, IAuditLogService auditLogService)
        {
            _logger = logger;
            _auditLogService = auditLogService;
            _lockingService = lockingService;
            _userAccessService = userAccessService;
        }

        // Post api/v1/[controller]/{id}/Lock
        [HttpPost]
        [Route("Lock")]
        [ProducesResponseType(typeof(LockingResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(UserAccessDenied), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(LockOpenSuccessful), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Lock([FromBody] OpenLockInfo openLockInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = new LockingResult();
            OpenLockResult lockingResult = new OpenLockResult();

            try
            {
                _logger.LogInformation($"User {openLockInfo.UserId} request {"Open"} Lock with Id= {openLockInfo.LockId}");

                Guid auditLogId = await _auditLogService.LogLockRequest(openLockInfo.LockId, 
                    openLockInfo.UserId, Model.LockCommand.Open);

                if (_userAccessService.HasAccess(openLockInfo.UserId, openLockInfo.LockId).Result)
                {
                    lockingResult = await _lockingService.OpenLock(openLockInfo.LockId);
                    var auditsult = await _auditLogService.LogLockResult(auditLogId, lockingResult);
                    if (lockingResult == OpenLockResult.Opened)
                    {
                        _logger.LogInformation($"Request {"Open"} for Lock with Id={openLockInfo.LockId} has been successful.");
                        return Ok(new LockOpenSuccessful());
                    }
                    else
                    {
                        _logger.LogWarning($"Request {"Open"} for Lock with Id={openLockInfo.LockId} has been faild.");
                        return Ok(new LockOpenFailed());
                    }
                }
                else
                {
                    var auditsult = await _auditLogService.LogLockResult(auditLogId, OpenLockResult.UserAccessDenied);
                    return Ok(new UserAccessDenied());
                }                                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Lock Service API");
            }
            return Ok(result);
        }
    }
}