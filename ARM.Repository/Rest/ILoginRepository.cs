using ARM.Entities;
using System.Threading.Tasks;

namespace ARM.Repository
{
    public interface ILoginRepository
    {
        Task<AuthenticatedUser> AuthenticateAsync(string username, string password);
    }
}