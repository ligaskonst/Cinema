using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Cinema.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Cinema.WebUI.Models;

namespace Cinema.WebUI.Controllers
{
    public class FilmController : Controller
    {
        private readonly ILogger<FilmController> _logger;
        private readonly IFilmService _filmService;

        public FilmController(ILogger<FilmController> logger,
            IFilmService filmService)
        {
            _logger = logger;
            _filmService = filmService;
        }

        public async Task<IActionResult> Page(int pageNumber = 1)
        {
            return View(await _filmService.GetFilms(pageNumber));
        }

        [HttpGet]
        public IActionResult NewFilm()
        {
            return View("AddEdit", new FilmFileModel());
        }

        [HttpPost]
        public async Task<IActionResult> NewFilm(FilmFileModel filmModel)
        {
            try
            {
                await SetImage(filmModel);
                await _filmService.CreateFilm(filmModel);

                return RedirectToAction("Page");
            }
            catch (Exception e)
            {
                ViewBag.MessageError = e.Message;
                return View("AddEdit", filmModel);
            }
        }

        [HttpGet("[controller]/[action]/{id}")]
        public async Task<IActionResult> Remove(string id)
        {
            try
            {
                await _filmService.RemoveFilm(Guid.Parse(id));
            }
            catch (Exception e)
            {
                ViewBag.MessageError = e.Message;
            }

            return RedirectToAction("Page");
        }

        [HttpGet("[controller]/[action]/{id}")]
        public async Task<IActionResult> EditFilm(string id)
        {
            var model = await _filmService.GetFilm(Guid.Parse(id));

            return View("AddEdit", new FilmFileModel()
            {
                Id = model.Id,
                Description = model.Description,
                ReleaseYear = model.ReleaseYear,
                User = model.User,
                Title = model.Title,
                Producer = model.Producer,
                Poster = "\\Images\\Movies\\" +model.Poster,
                UserId = model.UserId,
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditFilm(FilmFileModel filmModel)
        {
            try
            {
                await SetImage(filmModel);
                await _filmService.UpdateFilm(filmModel);

                return RedirectToAction("Page");
            }
            catch (Exception e)
            {
                ViewBag.MessageError = e.Message;

                return View("AddEdit", filmModel);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task SetImage(FilmFileModel filmModel)
        {
            if (filmModel.FormFile == null)
                return;

            await using MemoryStream stream = new MemoryStream();

            await filmModel.FormFile.CopyToAsync(stream);
            filmModel.File = stream.ToArray();
            filmModel.Poster = filmModel.FormFile.FileName;
        }
    }
}
