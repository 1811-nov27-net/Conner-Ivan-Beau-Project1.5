using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using VaporAPI.Library;

namespace VaporAPI.App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private SignInManager<IdentityUser> signInManager { get; }
        public IRepository Repo;

        public AccountController(IRepository repo,SignInManager<IdentityUser> _signInManager, IdentityDbContext db)
        {
            Repo = repo;
            db.Database.EnsureCreated();
            signInManager = _signInManager;
        }

        [HttpPost]
        public async Task<ActionResult> Login(AccountView account)
        {
            //insert logic with our repo here? Potentially pass the user back to them instead of NoContent
            

            var result = await signInManager.PasswordSignInAsync(account.UserName, account.Password,
                isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                //for if the login was invalid
                return StatusCode(403);
            }
            //User loggedUser = new User { UserName = account.UserName, Password = account.Password, Admin = 0};
            User loggedUser = Repo.GetUser(account.UserName);

            return CreatedAtRoute("GetUser", new { username = loggedUser.UserName }, loggedUser);
        }

        //use admin bool for permissions
        [HttpPost]
        public async Task<ActionResult> Register(AccountView account,
            [FromServices] UserManager<IdentityUser> userManager,
            [FromServices] RoleManager<IdentityRole> roleManager, bool admin = false)
        {

            //TODO:insert logic for our repo here - An AddUser statement
            var user = new IdentityUser(account.UserName);
            var checkUser = await userManager.CreateAsync(user, account.Password);

            if(!checkUser.Succeeded)
            {
                return BadRequest(checkUser); //means there was a problem like a duplicate username or an invalid character
            }

            if(admin)
            {
                var confirmRole = await roleManager.RoleExistsAsync("admin");
                if(!confirmRole)
                {
                    var adminRole = new IdentityRole("admin");
                    var checkRole = await roleManager.CreateAsync(adminRole);
                    if(!checkRole.Succeeded)
                    {
                        return StatusCode(500, checkRole);
                    }
                }
                //admin role now must exist
                var checkAddRole = await userManager.AddToRoleAsync(user, "admin");
                if(!checkAddRole.Succeeded)
                {
                    return StatusCode(500, checkAddRole);
                }
            }

            await signInManager.SignInAsync(user, isPersistent: false);

            User loggedUser = new User { UserName = account.UserName, Password = account.Password, Admin = admin};
            Repo.AddUser(loggedUser);

            return CreatedAtRoute("GetUser", new { username = loggedUser.UserName }, loggedUser);

            //TODO: might want to return the User created
           // return NoContent();
        }

        public async Task<NoContentResult> Logout()
        {
            await signInManager.SignOutAsync();

            return NoContent();
        }

        [HttpGet]
        [Authorize]
        public string LoggedInUser()
        {
            return User.Identity.Name;
        }


    }
}