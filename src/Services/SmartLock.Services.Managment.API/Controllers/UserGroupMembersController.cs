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
    public class UserGroupMembersController : ControllerBase
    {
        private readonly ManagmentDbContext _context;

        public UserGroupMembersController(ManagmentDbContext context)
        {
            _context = context;
        }

        // GET: api/UserGroupMembers
        [HttpGet]
        public IEnumerable<UserGroupMember> GetUserGroupMembers()
        {
            return _context.UserGroupMembers;
        }

        // GET: api/UserGroupMembers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserGroupMember([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userGroupMember = await _context.UserGroupMembers.FindAsync(id);

            if (userGroupMember == null)
            {
                return NotFound();
            }

            return Ok(userGroupMember);
        }

        // PUT: api/UserGroupMembers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserGroupMember([FromRoute] Guid id, [FromBody] UserGroupMember userGroupMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userGroupMember.UserGroupMemberId)
            {
                return BadRequest();
            }

            _context.Entry(userGroupMember).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserGroupMemberExists(id))
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

        // POST: api/UserGroupMembers
        [HttpPost]
        public async Task<IActionResult> PostUserGroupMember([FromBody] UserGroupMember userGroupMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UserGroupMembers.Add(userGroupMember);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserGroupMember", new { id = userGroupMember.UserGroupMemberId }, userGroupMember);
        }

        // DELETE: api/UserGroupMembers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserGroupMember([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userGroupMember = await _context.UserGroupMembers.FindAsync(id);
            if (userGroupMember == null)
            {
                return NotFound();
            }

            _context.UserGroupMembers.Remove(userGroupMember);
            await _context.SaveChangesAsync();

            return Ok(userGroupMember);
        }

        private bool UserGroupMemberExists(Guid id)
        {
            return _context.UserGroupMembers.Any(e => e.UserGroupMemberId == id);
        }
    }
}