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
    public class TagController : ControllerBase
    {

        public IRepository Repo;

        public TagController(IRepository repo)
        {
            Repo = repo;
        }

        // GET: api/Tag
        [HttpGet]
        public ActionResult<IEnumerable<Tag>> Get()
        {
            try
            {
                return Repo.GetTags().ToList();
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

        // GET: api/Tag/5
        [HttpGet("{id}", Name = "GetTag")]
        public ActionResult<Tag> Get(int id)
        {
            Tag tag;
            try
            {
                tag = Repo.GetTag(id);

            }
            catch (Exception)
            {

                return StatusCode(500);
            }
            if (tag == null)
            {
                return NotFound();
            }
            return tag;
        }

        // POST: api/Tag
        [HttpPost]
        public ActionResult Post([FromBody] Tag tag)
        {
            try
            {
                bool check = Repo.AddTag(tag);
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

            return CreatedAtRoute("GetTag", new { id = tag.TagId }, tag);
        }

        //// PUT: api/Tag/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
