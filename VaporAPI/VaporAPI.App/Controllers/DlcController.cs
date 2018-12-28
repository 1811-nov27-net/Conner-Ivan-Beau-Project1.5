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
    public class DlcController : ControllerBase
    {
        public IRepository Repo;

        public DlcController(IRepository repo)
        {
            Repo = repo;
        }
        //GET: api/Dlc
        [HttpGet]
        public ActionResult<string> Get()
        {
            try
            {
                return "nothing";//Repo.GetDlcs().ToList();
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

        // GET: api/Dlc/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Dlc> Get(int id)
        {
            Dlc dlc;
            try
            {
                dlc = Repo.GetDlc(id);

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

        [HttpGet("Game/{gameid}", Name = "Get")]
        public ActionResult<ICollection<Dlc>> Get(int gameid,int id)
        {
            List<Dlc> dlcs;
            try
            {
                dlcs = Repo.GetGameDlcs(gameid).ToList();

            }
            catch (Exception)
            {

                return StatusCode(500);
            }
            if (dlcs == null)
            {
                return NotFound();
            }
            return dlcs;
        }

        // POST: api/Dlc
        [HttpPost]
        public ActionResult Post([FromBody] Dlc dlc)
        {
            try
            {
                bool check = Repo.AddDlc(dlc);
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

            return CreatedAtRoute("Get", new { id = dlc.Dlcid }, dlc);
        }

        // PUT: api/Dlc/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Dlc value)
        {
            Dlc dlc;
            try
            {
                dlc = Repo.GetDlc(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
            if (dlc == null)
            {
                return NotFound();
            }
            if (dlc.Dlcid != value.Dlcid)
            {
                return BadRequest("cannot change ID");
            }
            try
            {
                Repo.UpdateDlc(value);
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
                var dlc = Repo.GetDlc(id);
                //for game doesnt exist
                if (dlc == null)
                {
                    return NotFound();
                }
                Repo.DeleteDlc(dlc.Dlcid);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
            return NoContent();
        }
    }
}
