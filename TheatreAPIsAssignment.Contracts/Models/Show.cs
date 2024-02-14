using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatreAPIsAssignment.Contracts.Models
{
    public class Show
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public Movie Movie { get; set; }
            
        public string TheatreName { get; set; }

        public string Time { get; set; }

        public int BookedSeats { get; set; } = 0;

    }
}
