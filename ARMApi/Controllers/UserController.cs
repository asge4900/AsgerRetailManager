using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ARMApi.Data;
using ARMApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;

namespace ARMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;

        public UserController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpGet]
        public void GetById()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //UserData data = new UserData();

            //return data.GetUserById(userId).First();
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Api/User/Admin/GetAllUsers")]
        public List<ApplicationUserModel> GetAllUsers()
        {
            List<ApplicationUserModel> output = new List<ApplicationUserModel>();

            var users = context.Users.ToList();
            var userRoles = from ur in context.UserRoles
                            join r in context.Roles on ur.RoleId equals r.Id
                            select new { ur.UserId, ur.RoleId, r.Name };

            foreach (var user in users)
            {
                ApplicationUserModel u = new ApplicationUserModel
                {
                    Id = user.Id,
                    Email = user.Email
                };

                u.Roles = userRoles.Where(x => x.UserId == u.Id).ToDictionary(key => key.RoleId, val => val.Name);

                output.Add(u);
            }

            return output;
         
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Api/User/Admin/GetAllRoles")]
        public Dictionary<string, string> GetAllRoles()
        {
            var roles = context.Roles.ToDictionary(x => x.Id, x => x.Name);

            return roles;           
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("api/User/Admin/AddRole")]
        public async Task AddRole(UserRolePairModel pairing)
        {
            var user = await userManager.FindByIdAsync(pairing.UserId);
            await userManager.AddToRoleAsync(user, pairing.RoleName);          
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("api/Admin/RemoveRole")]
        public async Task RemoveRole(UserRolePairModel pairing)
        {
            var user = await userManager.FindByIdAsync(pairing.UserId);
            await userManager.RemoveFromRoleAsync(user, pairing.RoleName);
        }
    }
}