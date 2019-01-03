using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VaporAPI.Library;

namespace VaporAPI.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReviewController : ControllerBase
    {

        public IRepository Repo;

        public ReviewController(IRepository repo)
        {
            Repo = repo;
        }

        // GET: api/Review
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Review/5
        /*
        [HttpGet("{UserName}/{id}", Name = "GetReviews")]
        public ActionResult<UserGame> GetUser(string username, int id)
        {
            UserGame review;
            try
            {
                review = Repo.GetReviewByUserGame(username, id);

            }
            catch (Exception)
            {
                //internal server exception
                return StatusCode(500);
            }
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }
        */

        // GET: api/Review/5
        [HttpGet("Game/{id}", Name = "GetGames")]
        public ActionResult<ICollection<UserGame>> GetGame(int id)
        {
            ICollection<UserGame> reviews;
            try
            {
                reviews = Repo.GetReviewsByGame(id);

            }
            catch (Exception)
            {
                //internal server exception
                return StatusCode(500);
            }
            if (reviews == null)
            {
                return NotFound();
            }
            return reviews.ToList();
        }

        [HttpGet("{username}/{id}", Name = "GetReview")]
        public ActionResult<FullUserGame> GetUserReview(string username, int id)
        {
            FullUserGame review;
            try
            {
                review = Repo.GetFullUserGame(username, id);

            }
            catch (Exception)
            {
                //internal server exception
                return StatusCode(500);
            }
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }

        // POST: api/Review
        [HttpPost]
        public ActionResult Post([FromBody] UserGame review)
        {
            try
            {
                bool check = Repo.AddReview(review);
                //check is for checking if a review already exists, ideally this will never be the case
                //and if it does return status code 409
                if (!check)
                {
                    return StatusCode(409);
                }
            }
            catch (Exception)
            {
                //internal server serror
                return StatusCode(500);
            }

            return CreatedAtRoute("GetReview", new { UserName = review.User.UserName }, review);
        }


        [HttpPost("{username}/{id}")]
        public ActionResult PostScoreReview(string username, int id, [FromBody] ScoreReview review)
        {
            try
            {
                bool check = Repo.AddReview(username,id,review);
                //check is for checking if a review already exists, ideally this will never be the case
                //and if it does return status code 409
                if (!check)
                {
                    return StatusCode(409);
                }
            }
            catch (Exception)
            {
                //internal server serror
                return StatusCode(500);
            }

            return CreatedAtRoute("GetReview", new { UserName = username }, review);
        }


        // PUT: api/Review/5
        [HttpPut("{username}/{id}")]
        public ActionResult Put(string username, int id, [FromBody] UserGame value)
        {
            UserGame review;
            try
            {
                review = Repo.GetUserGame(username, id);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
            if (review == null)
            {
                return NotFound();
            }
            if (review.User.UserName != value.User.UserName)
            {
                return BadRequest("cannot change ID");
            }
            try
            {
                Repo.UpdateReview(value);
            }
            catch (Exception ex)
            {
                // internal server error
                return StatusCode(500);
            }

            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{username}/{id}")]
        public ActionResult Delete(string username, int id)
        {
            try
            {
                var review = Repo.GetUserGame(username, id);
                //for game doesnt exist
                if (review == null)
                {
                    return NotFound();
                }
                Repo.DeleteReview(review);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
            return NoContent();
        }
    }
}
