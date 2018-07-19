using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SmartLock.Services.AuditLogs.API.Data;
using SmartLock.Services.AuditLogs.API.Helper;
using SmartLock.Services.AuditLogs.API.Model;

namespace SmartLock.Services.AuditLogs.API.Controllers
{
    [Route("api/v1/[controller]")]
    //[Authorize]
    [ApiController]
    public class AuditLogsController : ControllerBase
    {
        private readonly AuditLogDbContext _context;

        public AuditLogsController(AuditLogDbContext context)
        {
            _context = context;
        }

        // GET: api/AuditLogs
        [HttpGet]
        public async Task<IActionResult> GetAuditLogs([FromQuery]int? pageIndex, [FromQuery]int? pageSize)
        {
            //return _context.AuditLogs;
            IQueryable<AuditLog> auditLogs = from s in _context.AuditLogs
                                             orderby s.RequestDate descending
                                             select s;
            var data = await PaginatedList<AuditLog>.CreateAsync(
                auditLogs.AsNoTracking(), pageIndex ?? 1, pageSize ?? 10);

            return Ok(data);
        }

        // GET: api/AuditLogs/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetAuditLog([FromRoute] Guid id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var auditLog = await _context.AuditLogs.FindAsync(id);

        //    if (auditLog == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(auditLog);
        //}

        // PUT: api/AuditLogs/5
        [HttpPut("{id}/{result}")]
        public async Task<IActionResult> PutAuditLog([FromRoute] Guid id, [FromRoute] string result)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registerdAuditLog = await _context.AuditLogs.FindAsync(id);
            registerdAuditLog.CommandResult = result;
            _context.Entry(registerdAuditLog).State = EntityState.Modified;

            try
            {
                int r = _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuditLogExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AuditLogs
        [HttpPost]
        [Route("PostAuditLog")]
        public async Task<IActionResult> PostAuditLog([FromBody] AuditLog auditLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            auditLog.AuditLogId = Guid.NewGuid();
            _context.AuditLogs.Add(auditLog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostAuditLog", new { id = auditLog.AuditLogId }, auditLog.AuditLogId);
        }        

        private bool AuditLogExists(Guid id)
        {
            return _context.AuditLogs.Any(e => e.AuditLogId == id);
        }
    }
}