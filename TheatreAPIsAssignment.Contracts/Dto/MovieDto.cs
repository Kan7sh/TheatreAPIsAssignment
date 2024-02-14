using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatreAPIsAssignment.Contracts.Models;

namespace TheatreAPIsAssignment.Contracts.Dto
{
    public class MovieDto
    {

        public string Name { get; set; }

        public string Genre { get; set; }

        public int Runtime { get; set; }

        public string Director { get; set; }

        public bool Status { get; set; } = true;

    }
}
