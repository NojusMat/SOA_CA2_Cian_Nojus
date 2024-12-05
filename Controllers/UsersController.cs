using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOA_CA2_Cian_Nojus.Data;
using SOA_CA2_Cian_Nojus.Models;
using SOA_CA2_Cian_Nojus.DTOs;
using Asp.Versioning;
using SOA_CA2_Cian_Nojus.Authentication;

namespace SOA_CA2_Cian_Nojus.Controllers
{

    [Route("api/v{version:apiVersion}/users")]
    [ApiVersion("1.0")]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public class UsersController : ControllerBase
    {
        private readonly SOA_CA2_Cian_NojusContext _context;

        public UsersController(SOA_CA2_Cian_NojusContext context)
        {
            _context = context;
			context.Database.EnsureCreated();
		}

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUser()
        {
            return await _context.User.Select(u => new UserDTO
            {
                id = u.Id,
                email = u.email,
                isAdministrator = u.isAdministrator
            }).ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return new UserDTO
            {
                id = user.Id,
                email = user.email,
                isAdministrator = user.isAdministrator

            };
        }

        // GET: api/users/login/{email}
        [HttpGet("login/{email}")]
		public async Task<ActionResult<UserDTO>> GetUserByEmail(string email)
		{
			// code to find an item by something other than ID was created with help from: https://learn.microsoft.com/en-us/ef/ef6/querying/

			var user = await _context.User.Where(u => u.email == email).FirstOrDefaultAsync();

			if (user == null)
			{
				return NotFound();
			}

            return new UserDTO
            {
                id = user.Id,
                email = user.email,
                isAdministrator = user.isAdministrator
            };
		}

		// PUT: api/Users/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDTO userDTO)
        {
            if (id != userDTO.id)
            {
                return BadRequest();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.email = userDTO.email;
            user.isAdministrator = userDTO.isAdministrator;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(UserDTO userDTO)
        {
            var user = new User
            {
                email = userDTO.email,
                isAdministrator = userDTO.isAdministrator
            };
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            userDTO.id = user.Id;
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
