using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ARM.DataManager.Library.DataAccess;
using ARMApi.Data;
using ARMApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ARMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ILogger logger;

        public UserController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IUserData userData, ILogger logger)
        {
            this.context = context;
            this.userManager = userManager;
            this.logger = logger;
        }

        [HttpGet]
        public void GetById()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //return userData.GetUserById(userId).First();
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Admin/GetAllUsers")]
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
        [Route("Admin/GetAllRoles")]
        public Dictionary<string, string> GetAllRoles()
        {
            var roles = context.Roles.ToDictionary(x => x.Id, x => x.Name);

            return roles;           
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Admin/AddRole")]
        public async Task AddRole(UserRolePairModel pairing)
        {
            //string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var loggedInUser = userData.GetUserById(loggedInUserId).First();

            var user = await userManager.FindByIdAsync(pairing.UserId);
            await userManager.AddToRoleAsync(user, pairing.RoleName);          
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Admin/RemoveRole")]
        public async Task RemoveRole(UserRolePairModel pairing)
        {
            //string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var user = await userManager.FindByIdAsync(pairing.UserId);
            await userManager.RemoveFromRoleAsync(user, pairing.RoleName);
        }
    }
}