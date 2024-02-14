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
    public class ShowServices:IShowServices
    {

        private readonly DataContext _context;



        public ShowServices(DataContext context)
        {
            _context = context;

        }

        public async Task<bool> AddShow(Show show)
        {
  
            _context.Shows.Add(show);
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

        public async Task<bool> CheckShowExists(Show show)
        {
            var existingShow = await _context.Shows
                                        .FirstOrDefaultAsync(s => s.MovieId == show.MovieId && s.Time.ToLower() == show.Time.ToLower());

            return existingShow != null;
        }

        public async Task<Show?> GetShowById(int showId)
        {
            return await _context.Shows.Where(o => o.Id == showId).FirstOrDefaultAsync();

        }

        public async Task<ICollection<Show>> GetShows(int movieId)
        {
            return await _context.Shows.Where(o => o.Movie.Id == movieId).ToListAsync();

        }

        public bool IsValidTimings(string time)
        {
            return time.ToLower() == "morning" || time.ToLower() == "afternoon" || time.ToLower() == "evening" || time.ToLower() == "night";
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> UpdateShow(Show show)
        {
            _context.Update(show);
            return await Save();
        }
    }
}
