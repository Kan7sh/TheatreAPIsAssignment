using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TheatreAPIsAssignment.Contracts.Context;
using TheatreAPIsAssignment.Contracts.Models;
using TheatreAPIsAssignment.Services.Interfaces;

namespace TheatreAPIsAssignment.Services.Services
{
    public class MovieServices:IMovieServices
    {
        private readonly DataContext _context;

        public MovieServices(DataContext context)
        {
            _context = context;

        }

        public async Task<bool> AddMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            return await Save();
        }
        public string? CheckForEmptyProperties<T>(T obj)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(int))
                {
                    continue;
                }
                var value = property.GetValue(obj);
                if (value == null || (value is string && string.IsNullOrWhiteSpace((string)value)))
                {
                    return $"The {property.Name} field is empty.";
                }
            }
            return null;
        }

        public async Task<bool> DeleteMovie(Movie movie)
        {
            _context.Remove(movie);
            return await Save();
        }

        public async Task<ICollection<Movie>> GetAllMovies()
        {
            return await _context.Movies.Where(m => m.Status).ToListAsync();
        }

        public  Movie? GetMovieById(int MovieId)
        {
            return  _context.Movies.Where(o=>o.Id==MovieId).FirstOrDefault();
        }

        public async Task<Movie?> GetMovieByName(string name)
        {
            var lowerCaseName = name.ToLower();

            return await _context.Movies
                .FirstOrDefaultAsync(m => m.Name.ToLower() == lowerCaseName);
        }

        public async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
            return await saved > 0 ? true : false;
        }

        public async Task<bool> UpdateMovie(Movie movie)
        {
            _context.Update(movie);

            return await Save();
        }
    }
}
