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
    public class UserController : ControllerBase
    {

        public IRepository Repo;

        public UserController(IRepository repo)
        {
            Repo = repo;
        }

        // GET: api/User
        //getting all the users
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            try
            {
                return Repo.GetUsers().ToList();
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

        // GET: api/User/5/Library
        //getting all the users games
        [HttpGet("{UserName}/Library", Name = "GetLibrary")]
        public ActionResult<IEnumerable<UserGame>> GetGames(string username)
        {
            try
            {
                return Repo.GetUserGames(username).ToList();
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

        // GET: api/User/5
        //getting the user with username
        [HttpGet("{UserName}", Name = "Get")]
        public ActionResult<User> Get(string UserName)
        {
            User user;
            try
            {
                user = Repo.GetUser(UserName);
                
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        // GET: api/User/5/Library/5
        [HttpGet("{UserName}/Library/{id}", Name = "GetGame")]
        public ActionResult<UserGame> GetGame(string UserName, int id)
        {
            UserGame game;
            try
            {
                game = Repo.GetUserGame(UserName,id);

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


        // POST: api/User
        [HttpPost]
        public ActionResult Post([FromBody] User user)
        {
            try
            {
                bool check = Repo.AddUser(user);
                //check is for checking if the username already exists, and if it does return status code 409
                if(!check)
                {
                    return StatusCode(409);
                }
            }
            catch (Exception)
            {

                return StatusCode(500);
            }

            return CreatedAtRoute("Get", new { UserName= user.UserName }, user);
        }


        // POST: api/User/5/Library
        [HttpPost("{UserName}/Library", Name = "Post")]
        public ActionResult PostGame(string UserName,[FromBody] UserGame game)
        {
            try
            {
                bool check = Repo.AddUserGame(game);
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

            return CreatedAtRoute("GetLibrary", new { Game = game.Game.GameId }, game);
        }








        // PUT: api/User/5
        [HttpPut("{UserName}")]
        public ActionResult Put(string UserName, [FromBody] User value)
        {
            User user;
            try
            {
                user = Repo.GetUser(UserName);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
            if (user == null)
            {
                return NotFound();
            }
            if (user.UserName != value.UserName)
            {
                return BadRequest("cannot change ID");
            }
            try
            {
                Repo.UpdateUser(value);
            }
            catch (Exception ex)
            {
                // internal server error
                return StatusCode(500);
            }

            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{UserName}")]
        public ActionResult Delete(string UserName)
        {
            try
            {
                var user = Repo.GetUser(UserName);
                //for user doesnt exist
                if (user == null)
                {
                    return NotFound();
                }
                Repo.DeleteUser(user.UserName);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
            return NoContent();
        }
    }
}
