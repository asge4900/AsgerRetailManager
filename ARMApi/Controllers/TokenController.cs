using ARMApi.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ARMApi.Controllers
{
    public class TokenController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;

        public TokenController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        private async Task<bool> IsValidUsernameAndPassword(string username, string password)
        {
            var user = await userManager.FindByEmailAsync(username);
            return await userManager.CheckPasswordAsync(user, password);
        }
    }
}