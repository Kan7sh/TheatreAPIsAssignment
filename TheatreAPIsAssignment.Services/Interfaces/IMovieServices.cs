using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatreAPIsAssignment.Contracts.Models;

namespace TheatreAPIsAssignment.Services.Interfaces
{
    public interface IMovieServices
    {
        public Task<bool> AddMovie(Movie movie);

        public Movie? GetMovieById(int MovieId);

        public Task<ICollection<Movie>> GetAllMovies();

        public Task<bool> DeleteMovie(Movie movie);

        public Task<Movie?> GetMovieByName(string name);

        public Task<bool> UpdateMovie(Movie movie);

        public string? CheckForEmptyProperties<T>(T obj);

    }
}
