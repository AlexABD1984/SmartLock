using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartLock.Services.Managment.API.Data;
using SmartLock.Services.Managment.API.Model;

namespace SmartLock.Services.Managment.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LocksController : ControllerBase
    {
        private readonly ManagmentDbContext _context;

        public LocksController(ManagmentDbContext context)
        {
            _context = context;
        }

        // GET: api/Locks
        [HttpGet]
        public IEnumerable<Lock> GetLocks()
        {
            return _context.Locks;
        }

        // GET: api/Locks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLock([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var @lock = await _context.Locks.FindAsync(id);

            if (@lock == null)
            {
                return NotFound();
            }

            return Ok(@lock);
        }

        // PUT: api/Locks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLock([FromRoute] Guid id, [FromBody] Lock @lock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @lock.LockId)
            {
                return BadRequest();
            }

            _context.Entry(@lock).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LockExists(id))
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

        // POST: api/Locks
        [HttpPost]
        public async Task<IActionResult> PostLock([FromBody] Lock @lock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Locks.Add(@lock);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLock", new { id = @lock.LockId }, @lock);
        }

        // DELETE: api/Locks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLock([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var @lock = await _context.Locks.FindAsync(id);
            if (@lock == null)
            {
                return NotFound();
            }

            _context.Locks.Remove(@lock);
            await _context.SaveChangesAsync();

            return Ok(@lock);
        }

        private bool LockExists(Guid id)
        {
            return _context.Locks.Any(e => e.LockId == id);
        }
    }
}