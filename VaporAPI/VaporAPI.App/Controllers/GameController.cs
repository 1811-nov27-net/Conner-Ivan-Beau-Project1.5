using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VaporAPI.Library;

namespace VaporAPI.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {

        public IRepository Repo;

        public GameController(IRepository repo)
        {
            Repo = repo;
        }

        // GET: api/Game
        [HttpGet]
        public ActionResult<IEnumerable<Game>> Get()
        {
            try
            {
                return Repo.GetGames().ToList();
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

        // GET: api/Game/5
        [HttpGet("{id}", Name = "GetGame")]
        public ActionResult<Game> Get(int id)
        {
            Game game;
            try
            {
                game = Repo.GetGame(id);

            }
            catch (Exception)
            {

                return StatusCode(500);
            }
            if (game == null)
            {
                return NotFound();
            }
            return game;
        }

        // GET: api/Game/searchString
        [Route("GetGameSearch")]
        [HttpGet("{searchString}", Name = "GetGameSearch")]
        public ActionResult<IEnumerable<Game>> GetGameSearch(string searchString)
        {
            try
            {
                return Repo.GetGameBySearchName(searchString);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }


        // POST: api/Game
        [HttpPost]
        public ActionResult Post([FromBody] Game game)
        {
            try
            {
                bool check = Repo.AddGame(game);
                //check is for checking if the username already exists, and if it does return status code 409
                if (!check)
                {
                    return StatusCode(409);
                }
            }
            catch (Exception)
            {

                return StatusCode(500);
            }

            return CreatedAtRoute("GetGame", new { id = game.GameId }, game);
        }

        // PUT: api/Game/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Game value)
        {
            Game game;
            try
            {
                game = Repo.GetGame(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
            if (game == null)
            {
                return NotFound();
            }
            if (game.GameId != value.GameId)
            {
                return BadRequest("cannot change ID");
            }
            try
            {
                Repo.UpdateGame(value);
            }
            catch (Exception ex)
            {
                // internal server error
                return StatusCode(500);
            }

            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var game = Repo.GetGame(id);
                //for game doesnt exist
                if (game == null)
                {
                    return NotFound();
                }
                Repo.DeleteGame(game.GameId);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
            return NoContent();
        }
    }
}
