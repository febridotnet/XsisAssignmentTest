using Movies.Data.Models;

namespace Movies.Data.Repositories
{
    public interface IMovieRepository
    {
        Task<Movie> CreateMoviesAsync(Movie movie);
        Task DeleteMoviesAsync(Movie movie);
        Task<IEnumerable<Movie>> GetMoviesAsync();
        Task<Movie> GetMoviesByIdAsync(int id);
        Task UpdateMoviesAsync(Movie movie);
    }
}