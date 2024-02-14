using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatreAPIsAssignment.Contracts.Models;

namespace TheatreAPIsAssignment.Services.Interfaces
{
    public interface IShowServices
    {

        public Task<bool> AddShow(Show show);

        public Task<ICollection<Show>> GetShows(int showId);

        public Task<bool> CheckShowExists(Show show);

        public Task<Show?> GetShowById(int showId);

        public Task<bool> UpdateShow(Show show);

        public bool IsValidTimings(string time);
        public string? CheckForEmptyProperties<T>(T obj);

    }
}
