using System.Threading.Tasks;
using Cinema.Domain.Identity;
using Cinema.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Cinema.WebUI.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public SecurityService(IHttpContextAccessor httpContextAccessor, UserManager<UserModel> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<UserModel> GetCurrentUser()
            => await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

        public bool IsAuthenticated()
            => _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
    }
}
