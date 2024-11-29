using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOA_CA2_Cian_Nojus.Authentication;
using SOA_CA2_Cian_Nojus.Data;
using SOA_CA2_Cian_Nojus.Models;
using SOA_CA2_Cian_Nojus.DTOs;
using Asp.Versioning;

namespace SOA_CA2_Cian_Nojus.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public class PlatformsController : ControllerBase
    {
        private readonly SOA_CA2_Cian_NojusContext _context;

        public PlatformsController(SOA_CA2_Cian_NojusContext context)
        {
            _context = context;
			context.Database.EnsureCreated();
		}

        // GET: api/Platforms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Platform>>> GetPlatform()
        {

            var platform = await _context.Platform.ToListAsync();

            var platformDTO = platform.Select(p => new Platform
            {
                Id = p.Id,
                name = p.name,
                manufacturer = p.manufacturer
            }).ToList();

            return platformDTO;

        }

        // GET: api/Platforms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlatformDTO>> GetPlatform(int id)
        {
            var platform = await _context.Platform.FindAsync(id);

            if (platform == null)
            {
                return NotFound();
            }

            var platformDTO = new PlatformDTO
            {
                Id = platform.Id,
                name = platform.name,
                manufacturer = platform.manufacturer
            };

            return platformDTO;
        }

        // PUT: api/Platforms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlatform(int id, PlatformDTO platformDTO)
        {
            if (id != platformDTO.Id)
            {
                return BadRequest();
            }

            var platform = await _context.Platform.FindAsync(id);
            if (platform == null)
            {
                return NotFound();
            }

            platform.name = platformDTO.name;
            platform.manufacturer = platformDTO.manufacturer;

            _context.Entry(platform).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlatformExists(id))
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

        // POST: api/Platforms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlatformDTO>> PostPlatform(PlatformDTO platformDTO)
        {
            var platform = new Platform
            {
                name = platformDTO.name,
                manufacturer = platformDTO.manufacturer
            };

            _context.Platform.Add(platform);
            await _context.SaveChangesAsync();


            platformDTO.Id = platform.Id;
            return CreatedAtAction("GetPlatform", new { id = platform.Id }, platformDTO);
        }

        // DELETE: api/Platforms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlatform(int id)
        {
            var platform = await _context.Platform.FindAsync(id);
            if (platform == null)
            {
                return NotFound();
            }

            _context.Platform.Remove(platform);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlatformExists(int id)
        {
            return _context.Platform.Any(e => e.Id == id);
        }
    }
}
