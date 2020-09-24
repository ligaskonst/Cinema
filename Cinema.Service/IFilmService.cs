using System;
using System.Threading.Tasks;
using Cinema.Domain;

namespace Cinema.Service
{
    public interface IFilmService
    {
        Task<PaginatedList<FilmModel>> GetFilms(int pageNumber);

        Task UpdateFilm(FilmModel model);

        Task RemoveFilm(Guid id);

        Task CreateFilm(FilmModel model);

        Task<FilmModel> GetFilm(Guid id);
    }
}
