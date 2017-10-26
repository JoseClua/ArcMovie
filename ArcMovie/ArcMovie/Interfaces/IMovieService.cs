using System.Collections.Generic;
using System.Threading.Tasks;
using ArcMovie.Models;

namespace ArcMovie.Interfaces
{
    public interface IMovieService
    {
        Task<SearchMovie> SearchMoviesAsync(string searchTerm, int page);

        Task<SearchMovie> GetMoviesByCategoryAsync(int page, Enums.MovieCategory sortBy);

        Task<MovieDetail> GetMovieDetailAsync(int id);

        Task<List<Genre>> GetGenresAsync();
    }
}