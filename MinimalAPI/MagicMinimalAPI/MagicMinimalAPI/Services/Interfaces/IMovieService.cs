using MagicMinimalAPI.Models;

namespace MagicMinimalAPI.Services.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<Movies>> GetAllMoviesAsync();
        Task<Movies> GetMoviesAsync(int id);
        Task AddMovieAsync(Movies movie);
        Task UpdateMoviesAsync(int id, Movies movie);
        Task DeleteMoviesAsync(int id);
    }
}
