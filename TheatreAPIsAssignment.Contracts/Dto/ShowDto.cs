using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatreAPIsAssignment.Contracts.Models;

namespace TheatreAPIsAssignment.Contracts.Dto
{
    public class ShowDto
    {


        public int MovieId { get; set; }

        public string TheatreName { get; set; }

        public string Time { get; set; }

    }
}
