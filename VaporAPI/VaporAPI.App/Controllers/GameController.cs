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
        [Route("Search")]
        [HttpGet("Search/{searchString}", Name = "Search")]
        public ActionResult<IEnumerable<Game>> Search(string searchString)
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

        [Route("Filter")]
        [HttpGet("storageArray", Name = "Filter")]
        public ActionResult<IEnumerable<Game>> Filter(int[][] storageArray)
        {
            try
            {
                int[] price = storageArray[0];
                int[] rating = storageArray[1];
                int[] devId = storageArray[2];
                int[] tagId = storageArray[3];

                ICollection<Game> gamesPrice = Repo.GetBetweenPriceGames(price);
                ICollection<Game> gamesRating = Repo.GetBetweenRatingsGames(rating);
                ICollection<Game> gamesDev = Repo.GetGamesByDeveloper(devId);
                ICollection<Game> gamesTag = Repo.GetGamesByTags(tagId);

                ICollection<Library.Game>[] gamesArray = new ICollection<Game>[4];
                gamesArray[0] = gamesPrice;
                gamesArray[1] = gamesRating;
                gamesArray[2] = gamesDev;
                gamesArray[3] = gamesTag;

                var games = Repo.FilterGames(gamesArray).ToList();
                return games;
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
