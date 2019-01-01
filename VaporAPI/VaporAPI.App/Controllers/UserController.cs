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



        // GET: api/User/5/Library/Dlc
        //getting all the users games
        [HttpGet("{UserName}/Library/Dlc", Name = "GetLibraryDlc")]
        public ActionResult<IEnumerable<UserDlc>> GetDlcs(string username)
        {
            try
            {
                return Repo.GetUserDlcs(username).ToList();
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }




        // GET: api/User/5
        //getting the user with username
        [HttpGet("{UserName}", Name = "GetUser")]
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
        [HttpGet("{UserName}/Library/{id}", Name = "GetUserGame")]
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

        // GET: api/User/5/Library/5
        [HttpGet("{UserName}/Library/Dlc/{id}", Name = "GetUserDlc")]
        public ActionResult<UserDlc> GetDlc(string UserName, int id)
        {
            UserDlc dlc;
            try
            {
                dlc = Repo.GetUserDlc(UserName, id);

            }
            catch (Exception)
            {

                return StatusCode(500);
            }
            if (dlc == null)
            {
                return NotFound();
            }
            return dlc;
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

            return CreatedAtRoute("GetUser", new { user.UserName }, user);
        }


        // POST: api/User/5/Library
        [HttpPost("{UserName}/Library/{id}", Name = "PostGame")]
        public ActionResult PostGame(string username,int id, [FromBody]DateTime purchaseDate)
        {
            UserGame userGame;
            try
            {
                User user = Repo.GetUser(username);
                Game game = Repo.GetGame(id);
                if(user == null || game == null)
                {
                    return StatusCode(400);
                }
                userGame = new UserGame { User = user, Game = game, PurchaseDate = purchaseDate };
                bool check = Repo.AddUserGame(userGame);
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

            return CreatedAtRoute("GetUserGame", new { username=userGame.User.UserName,id=userGame.Game.GameId }, userGame);
        }

        // POST: api/User/5/Library
        [HttpPost("{UserName}/Library/Dlc", Name = "PostDlc")]
        public ActionResult PostDlc(string username, [FromBody]int id)
        {
            UserDlc userDlc;
            try
            {
                User user = Repo.GetUser(username);
                Dlc dlc = Repo.GetDlc(id);
                if (user == null || dlc == null)
                {
                    return StatusCode(400);
                }
                userDlc = new UserDlc { User = user, Dlc = dlc };
                bool check = Repo.AddUserDlc(userDlc);
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

            return CreatedAtRoute("GetUserDlc", new { username = userDlc.User.UserName, id = userDlc.Dlc.Dlcid }, userDlc);
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
