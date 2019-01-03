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
    public class DeveloperController : ControllerBase
    {

        public IRepository Repo;

        public DeveloperController(IRepository repo)
        {
            Repo = repo;
        }

        // GET: api/Developer
        [HttpGet]
        public ActionResult<IEnumerable<Developer>> Get()
        {
            try
            {
                return Repo.GetDevelopers().ToList();
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

        // GET: api/Developer/5
        [HttpGet("{id}", Name = "GetDeveloper")]
        public ActionResult<Developer> Get(int id)
        {
            Developer developer;
            try
            {

                developer = Repo.GetDeveloper(id);

            }
            catch (Exception)
            {

                return StatusCode(500);
            }
            if (developer == null)
            {
                return NotFound();
            }
            return developer;
        }

        // POST: api/Developer
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Post([FromBody] Developer developer)
        {
            try
            {
                bool check = Repo.AddDeveloper(developer);
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

            return CreatedAtRoute("GetDeveloper", new { id = developer.DeveloperId}, developer);
        }

        // PUT: api/Developer/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult Put(int id, [FromBody] Developer value)
        {
            Developer developer;
            try
            {
                developer = Repo.GetDeveloper(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
            if (developer == null)
            {
                return NotFound();
            }
            if (developer.DeveloperId!= value.DeveloperId)
            {
                return BadRequest("cannot change ID");
            }
            try
            {
                Repo.UpdateDeveloper(value);
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
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                var developer = Repo.GetDeveloper(id);
                //for user doesnt exist
                if (developer == null)
                {
                    return NotFound();
                }
                Repo.DeleteDeveloper(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
            return NoContent();
        }
    }
}
