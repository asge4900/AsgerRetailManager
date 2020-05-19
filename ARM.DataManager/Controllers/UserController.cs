using ARM.DataManager.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Routing;

namespace ARM.DataManager.Controllers
{
    //[Authorize]
    public class UserController : ApiController
    {
        [HttpGet]
        public void GetById()
        {
            string userId = RequestContext.Principal.Identity.GetUserId();

            //UserData data = new UserData();

            //return data.GetUserById(userId).First();
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Api/User/Admin/GetAllUsers")]
        public List<ApplicationUser> GetAllUsers()
        {
            using (var context = new ApplicationDbContext())
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var users = userManager.Users.ToList();
                var roles = context.Roles.ToList();

                return users;
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Api/User/Admin/GetAllRoles")]
        public Dictionary<string, string> GetAllRoles()
        {
            using (var context = new ApplicationDbContext())
            {                
                var roles = context.Roles.ToDictionary(x => x.Id, x => x.Name);

                return roles;
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("api/User/Admin/AddRole")]
        public async Task AddRole(UserRolePairModel pairing)
        {
            using (var context = new ApplicationDbContext())
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                await userManager.AddToRoleAsync(pairing.UserId, pairing.RoleName);
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("api/Admin/RemoveRole")]
        public async Task RemoveRole(UserRolePairModel pairing)
        {
            using (var context = new ApplicationDbContext())
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                await userManager.RemoveFromRoleAsync(pairing.UserId, pairing.RoleName);
            }
        }
    }
}
