using Cinema.Domain;
using Microsoft.AspNetCore.Http;

namespace Cinema.WebUI.Models
{
    public class FilmFileModel : FilmModel
    {
        public IFormFile FormFile { get; set; }
    }
}
