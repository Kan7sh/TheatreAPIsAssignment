using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TheatreAPIsAssignment.Contracts.Models
{
    public class Response<T> where T : class
    {
        public T Data { get; set; }
        public Error Error { get; set; }
        public HttpStatusCode Status { get; set; }
    }
    public class Error
    {
        public string ErrorMessage { get; set; }
    }
}
