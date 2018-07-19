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
    [Route("api/[controller]")]
    [ApiController]
    public class UserGroupsController : ControllerBase
    {
        private readonly ManagmentDbContext _context;

        public UserGroupsController(ManagmentDbContext context)
        {
            _context = context;
        }

        // GET: api/UserGroups
        [HttpGet]
        public IEnumerable<UserGroup> GetGroups()
        {
            return _context.Groups;
        }

        // GET: api/UserGroups/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserGroup([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userGroup = await _context.Groups.FindAsync(id);

            if (userGroup == null)
            {
                return NotFound();
            }

            return Ok(userGroup);
        }

        // PUT: api/UserGroups/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserGroup([FromRoute] Guid id, [FromBody] UserGroup userGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userGroup.UserGroupId)
            {
                return BadRequest();
            }

            _context.Entry(userGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserGroupExists(id))
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

        // POST: api/UserGroups
        [HttpPost]
        public async Task<IActionResult> PostUserGroup([FromBody] UserGroup userGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Groups.Add(userGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserGroup", new { id = userGroup.UserGroupId }, userGroup);
        }

        // DELETE: api/UserGroups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserGroup([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userGroup = await _context.Groups.FindAsync(id);
            if (userGroup == null)
            {
                return NotFound();
            }

            _context.Groups.Remove(userGroup);
            await _context.SaveChangesAsync();

            return Ok(userGroup);
        }

        private bool UserGroupExists(Guid id)
        {
            return _context.Groups.Any(e => e.UserGroupId == id);
        }
    }
}