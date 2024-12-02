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
    [ApiVersion("2.0")]
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
        public async Task<ActionResult<IEnumerable<PlatformDTO>>> GetPlatform()
        {

            var platform = await _context.Platform.Include(gp => gp.GamePlatforms).ThenInclude(g => g.Game).ToListAsync();

            var platformDTO = platform.Select(p => new PlatformDTO
            {
                id = p.Id,
                name = p.name,
                manufacturer = p.manufacturer,
                games = p.GamePlatforms.Select(p => p.Game.title).ToList()
            }).ToList();

            return platformDTO;

        }

        // GET: api/Platforms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlatformDTO>> GetPlatform(int id)
        {
            var platform = await _context.Platform.Include(gp => gp.GamePlatforms).ThenInclude(g => g.Game).FirstOrDefaultAsync(d => d.Id == id);

            if (platform == null)
            {
                return NotFound();
            }

            var platformDTO = new PlatformDTO
            {
                id = platform.Id,
                name = platform.name,
                manufacturer = platform.manufacturer,
                games = platform.GamePlatforms.Select(p => p.Game.title).ToList()
            };

            return platformDTO;
        }

        // PUT: api/Platforms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlatform(int id, PlatformDTO platformDTO)
        {
            if (id != platformDTO.id)
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

            _context.GamePlatform.RemoveRange(platform.GamePlatforms);
            if(platformDTO.games != null)
            {
                foreach (var gameDTO in platformDTO.games)
                {
                    var game = await _context.Games.FindAsync(gameDTO);
                    if (game == null)
                    {
                        return NotFound();
                    }
                    platform.GamePlatforms.Add(new GamePlatform { Game = game, Platform = platform });
                }
            }

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

            if (platformDTO.games != null)
            {
                foreach (var gameDTO in platformDTO.games)
                {
                    var game = await _context.Games.FindAsync(gameDTO);
                    if (game != null)
                    {
                        platform.GamePlatforms.Add(new GamePlatform { Game = game, Platform = platform });
                    }
                }
            }

            await _context.SaveChangesAsync();


            var createdPlatformDTO = new PlatformDTO
            {
                id = platform.Id,
                name = platform.name,
                manufacturer = platform.manufacturer,
            };

            return CreatedAtAction("GetPlatform", new { id = platform.Id }, createdPlatformDTO);
        }

        // DELETE: api/Platforms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlatform(int id)
        {
            var platform = await _context.Platform.Include(gp => gp.GamePlatforms).FirstOrDefaultAsync(p => p.Id == id);

            if (platform == null)
            {
                return NotFound();
            }

            _context.GamePlatform.RemoveRange(platform.GamePlatforms);

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
