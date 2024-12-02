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
    public class GamesController : ControllerBase
    {
        private readonly SOA_CA2_Cian_NojusContext _context;

        public GamesController(SOA_CA2_Cian_NojusContext context)
        {
            _context = context;
			context.Database.EnsureCreated();
		}

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetGames()
        {
            var games = await _context.Games.Include(d => d.Developer).Include(gp => gp.GamePlatforms).ThenInclude(p => p.Platform).ToListAsync();

            var gameDTO = games.Select(d => new GameDTO
            {
                id = d.Id,
                title = d.title,
                genre = d.genre,
                release_year = d.release_year,
                developer_id = d.developer_id,
                platforms = d.GamePlatforms.Select(d => d.Platform.Id).ToList()
            }).ToList();

            return gameDTO;
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDTO>> GetGames(int id)
        {
   
            var game = await _context.Games.Include(d => d.Developer).Include(gp => gp.GamePlatforms).ThenInclude( p => p.Platform).FirstOrDefaultAsync(d => d.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            var gameDTO = new GameDTO
            {
                id = game.Id,
                title = game.title,
                genre = game.genre,
                release_year = game.release_year,
                developer_id = game.developer_id,
                platforms = game.GamePlatforms.Select(d => d.Platform.Id).ToList()
            };

            return gameDTO;
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGames(int id, GameDTO gameDTO)
        {
            if (id != gameDTO.id)
            {
                return BadRequest();
            }
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            game.title = gameDTO.title;
            game.genre = gameDTO.genre;
            game.release_year = gameDTO.release_year;
            game.developer_id = gameDTO.developer_id;


            _context.GamePlatform.RemoveRange(game.GamePlatforms);

            if(gameDTO.platforms != null)
            {
                foreach (var platformDTO in gameDTO.platforms)
                {
                    var platform = await _context.Platform.FindAsync(platformDTO);
                    if (platform != null)
                    {
                        _context.GamePlatform.Add(new GamePlatform
                        {
                            GameId = game.Id,
                            PlatformId = platform.Id
                        });
                    }
                }
            }

            _context.Entry(game).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GamesExists(id))
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

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Games>> PostGames(GameDTO gameDTO)
        {
            var game = new Games
            {
                title = gameDTO.title,
                genre = gameDTO.genre,
                release_year = gameDTO.release_year,
                developer_id = gameDTO.developer_id,
            };

            _context.Games.Add(game);
            await _context.SaveChangesAsync();

            if (gameDTO.platforms != null)
            {
                foreach (var platformDTO in gameDTO.platforms)
                {
                    var platform = await _context.Platform.FindAsync(platformDTO);
                    if (platform != null)
                    {
                        _context.GamePlatform.Add(new GamePlatform
                        {
                            GameId = game.Id,
                            PlatformId = platform.Id
                        });
                    }
                }
            }

            await _context.SaveChangesAsync();

            var createdGameDTO = new GameDTO
            {
                id = game.Id,
                title = game.title,
                genre = game.genre,
                release_year = game.release_year,
                developer_id = game.developer_id,
            };

            return CreatedAtAction("GetGames", new { id = game.Id }, createdGameDTO);
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGames(int id)
        {
            var game = await _context.Games.Include(gp => gp.GamePlatforms).FirstOrDefaultAsync(g => g.Id == id);


            if (game == null)
            {
                return NotFound();
            }

            _context.GamePlatform.RemoveRange(game.GamePlatforms);

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GamesExists(int id)
        {
            return _context.Games.Any(g => g.Id == id);
        }
    }
}
