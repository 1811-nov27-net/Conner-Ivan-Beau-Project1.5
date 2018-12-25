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
        [HttpGet("User/{UserName}", Name = "Get")]
        public ActionResult<UserGame> GetUser(string username)
        {
            UserGame review;
            try
            {
                review = Repo.GetReviewbyUser(username);

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
            return review;
        }

        // GET: api/Review/5
        [HttpGet("Game/{id}", Name = "Get")]
        public ActionResult<ICollection<UserGame>> GetGame(int id)
        {
            ICollection<UserGame> reviews;
            try
            {
                reviews = Repo.GetReviewbyGame(id);

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

            return CreatedAtRoute("Get", new { UserName = review.User.UserName }, review);
        }

        // PUT: api/Review/5
        [HttpPut("{id}")]
        public ActionResult Put(string username, [FromBody] UserGame value)
        {
            UserGame review;
            try
            {
                review = Repo.GetReviewbyUser(username);
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
                Repo.UpdateReviewbyScore(value);
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
        public ActionResult Delete(string username)
        {
            try
            {
                var review = Repo.GetReviewbyUser(username);
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
