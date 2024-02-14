using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatreAPIsAssignment.Contracts.Models
{


    public class Movie
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Genre { get; set; }

        public int Runtime { get; set; }

        public string Director { get; set; }

        public bool Status { get; set; } = true;

        public ICollection<Show> Shows { get; set; } = [];

    }
}
