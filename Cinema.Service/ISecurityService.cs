using System.Threading.Tasks;
using Cinema.Domain.Identity;

namespace Cinema.Service
{
    public interface ISecurityService
    {
        Task<UserModel> GetCurrentUser();
    }
}
