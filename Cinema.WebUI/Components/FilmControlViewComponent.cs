using System.Threading.Tasks;
using Cinema.Domain;
using Cinema.Domain.Identity;
using Cinema.Service;
using Cinema.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WebUI.Components
{
    public class FilmControlViewComponent : ViewComponent
    {
        private readonly ISecurityService _securityService;

        public FilmControlViewComponent(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        public async Task<IViewComponentResult> InvokeAsync(FilmModel filmModel)
        {
            UserModel user = await _securityService.GetCurrentUser();
            FilmControlModel model = new FilmControlModel()
            {
                Id = filmModel.Id,
                HasAccess = user?.Id == filmModel.UserId

            };
            return View(model);
        }
    }
}
