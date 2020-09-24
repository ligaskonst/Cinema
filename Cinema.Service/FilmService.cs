using System;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using Cinema.Domain;
using Cinema.Repository;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Service
{
    public class FilmService : IFilmService
    {
        private readonly IGenericRepository<FilmModel> _genericRepository;
        private readonly IFileUploadService _fileUploadService;
        private readonly ISecurityService _securityService;

        public FilmService(IGenericRepository<FilmModel> genericRepository, 
            IFileUploadService fileUploadService, ISecurityService securityService)
        {
            _genericRepository = genericRepository;
            _fileUploadService = fileUploadService;
            _securityService = securityService;
        }

        public async Task<PaginatedList<FilmModel>> GetFilms(int pageNumber)
        {
            int pageSize = 5;
            int count = await _genericRepository.Get().CountAsync();
            var films = await _genericRepository.Get().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            
            return new PaginatedList<FilmModel>(films, count, pageNumber, pageSize);
        }

        public async Task UpdateFilm(FilmModel model)
        {
            if (model.UserId != (await _securityService.GetCurrentUser()).Id)
                throw new SecurityException("The current user cannot edit files.");

            if (string.IsNullOrEmpty(model.Title) ||
                string.IsNullOrEmpty(model.Description) ||
                string.IsNullOrEmpty(model.Poster) ||
                string.IsNullOrEmpty(model.Producer) ||
                model.UserId == 0 || model.ReleaseYear == 0)
            {
                throw new NullReferenceException("Required fields are missing.");
            }

            await _genericRepository.Update(model);
        }

        public async Task RemoveFilm(Guid id)
        {
            var film = await _genericRepository.FindById(id);
            if (film != null && film.UserId == (await _securityService.GetCurrentUser()).Id)
            {
                await _genericRepository.Remove(film);
                _fileUploadService.RemoveFile(GetFileToPath(film.Poster));
            }
        }

        public async Task CreateFilm(FilmModel model)
        {
            if (string.IsNullOrEmpty(model.Title) ||
                string.IsNullOrEmpty(model.Description) ||
                string.IsNullOrEmpty(model.Poster) ||
                string.IsNullOrEmpty(model.Producer) ||
                model.ReleaseYear == 0)
            {
                throw new NullReferenceException("Required fields are missing.");
            }

            if (model.Id != Guid.Empty && await _genericRepository.FindById(model.Id) != null)
            {
                throw new ArgumentException("A movie with the specified ID already exists.");
            }

            model.Id = Guid.NewGuid();
            model.Poster = $"{model.Id}_{model.Poster}";
            model.UserId = (await _securityService.GetCurrentUser()).Id;

            await _genericRepository.Create(model);
            await _fileUploadService.UploadFile(GetFileToPath(model.Poster), model.File);
        }

        public async Task<FilmModel> GetFilm(Guid id)
            => await _genericRepository.FindById(id);

        private string GetFileToPath(string fileName)
        {
            string uploadFolderName = "\\Images\\Movies";

            return $"{uploadFolderName}\\{fileName}";
        } 
    }
}
