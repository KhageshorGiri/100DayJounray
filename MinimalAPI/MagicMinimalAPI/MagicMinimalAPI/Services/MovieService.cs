using MagicMinimalAPI.Models;
using MagicMinimalAPI.Services.Interfaces;

namespace MagicMinimalAPI.Services
{
    public class MovieService : IMovieService
    {
        public List<Movies> _allmovies;

        public MovieService()
        {
            _allmovies = new List<Movies>()
            {
                new Movies{Id = 1, MovieName = "Targen", ReleaseYear = new DateTime(2000, 1, 1), Description="Action Movie"},
                new Movies{Id = 2, MovieName = "Avenger", ReleaseYear = new DateTime(2019, 1, 1), Description="Action Movie"},
                new Movies{Id = 3, MovieName = "Inception", ReleaseYear = new DateTime(2010, 1, 1), Description="Skic-Fic"},
             
            };
        }

        public async Task AddMovieAsync(Movies movie)
        {
            movie.Id = _allmovies.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
            _allmovies.Add(movie);
        }

        public Task DeleteMoviesAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Movies>> GetAllMoviesAsync()
        {
            return _allmovies;
        }

        public async Task<Movies> GetMoviesAsync(int id)
        {
            return _allmovies.FirstOrDefault(m => m.Id == id);
        }

        public Task UpdateMoviesAsync(int id, Movies movie)
        {
            throw new NotImplementedException();
        }
    }
}
