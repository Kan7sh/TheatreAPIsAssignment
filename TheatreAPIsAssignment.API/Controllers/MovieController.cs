using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TheatreAPIsAssignment.Contracts.Dto;
using TheatreAPIsAssignment.Contracts.Models;
using TheatreAPIsAssignment.Services.Interfaces;


namespace TheatreAPIsAssignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {

        private readonly IMovieServices _movieServices;
        private readonly IShowServices _showServices;
        private readonly IMapper _mapper;
        public MovieController(IMovieServices movieServices,IShowServices showServices, IMapper mapper)
        {
            _movieServices = movieServices;
            _showServices = showServices;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllMovies")]
        public async Task<Response<ICollection<Movie>>> GetAllMovies()
        {

            Response<ICollection<Movie>> response = new Response<ICollection<Movie>>();
            try
            {

                if (!ModelState.IsValid)
                {
                    response.Status = HttpStatusCode.BadRequest;
                    response.Error = new Error() { ErrorMessage = "Invalid request" };
                    return response;
                }
                var movies = await _movieServices.GetAllMovies();

                if (movies.Count == 0)
                {
                    response.Status = HttpStatusCode.NotFound;
                    response.Error = new Error() { ErrorMessage = "There are no movies" };
                    return response;
                }
             

                foreach (Movie movie in movies)
                {
                    ICollection<Show> showCollection = await _showServices.GetShows(movie.Id);

                    movie.Shows = showCollection;
                }

                response.Status = HttpStatusCode.OK;
                response.Data = movies;
    
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                response.Error = new Error() { ErrorMessage = "There was some error" };
                response.Status = HttpStatusCode.InternalServerError;

            }
            return response;

        }

        [HttpPost]
        [Route("AddMovie")]
        public async Task<Response<string>> PostMovie([FromBody] MovieDto movieDto)
        {


            Response<string> response = new Response<string>();
            try
            {


                if (!ModelState.IsValid)
                {
                    response.Status = HttpStatusCode.BadRequest;
                    response.Error = new Error() { ErrorMessage = "Invalid request" };
                    return response;
                }
                Movie movie = _mapper.Map<Movie>(movieDto);

                string? validateMessage = _movieServices.CheckForEmptyProperties(movie);

                if (validateMessage != null)
                {
                    response.Status = HttpStatusCode.BadRequest;
                    response.Error = new Error() { ErrorMessage = validateMessage };
                    return response;
                }
               
                Movie? movieByName = await _movieServices.GetMovieByName(movie.Name);

                if (movieByName != null)
                {
                    response.Status = HttpStatusCode.Conflict;
                    response.Error = new Error() { ErrorMessage = "Movie already exists" };
                    return response;

                }

                if (movie.Runtime == 0)
                {
                    response.Status = HttpStatusCode.BadRequest;
                    response.Error = new Error() { ErrorMessage = "Runtime can't be 0" };
                    return response;
                }

                bool result = await _movieServices.AddMovie(movie);

                if (result)
                {
                    response.Data = "Movie added successfully";
                    response.Status = HttpStatusCode.OK;
                }
                else
                {
                    response.Error = new Error() { ErrorMessage = "There was an error while adding the movie" };
                    response.Status = HttpStatusCode.InternalServerError;
                }


            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                response.Error = new Error() { ErrorMessage = "There was some error" };
                response.Status = HttpStatusCode.InternalServerError;

            }
            return response;

        }

        [HttpPut]
        [Route("UpdateStatus")]
        public async Task<Response<string>> PutMovie(int id, int status)
        {
            Response<string> response = new Response<string>();

            try
            {


                Movie? movie = _movieServices.GetMovieById(id);

                if (movie == null)
                {
                    response.Status = HttpStatusCode.NotFound;
                    response.Error = new Error() { ErrorMessage = "Movie Dosen't exists" };
                    return response;
                }


                if(status!=0&&status!=1)
                {
                    response.Status = HttpStatusCode.BadRequest;
                    response.Error = new Error() { ErrorMessage = "Invalid status" };
                    return response;
                }
                movie.Status = status == 1 ? true : false;
                bool result = await _movieServices.UpdateMovie(movie);

                if(result)
                {
                    response.Data = $"Movie {(status == 1 ? "Activated" : "Deactivated")} successfully";
                    response.Status = HttpStatusCode.OK;
                }
                else
                {
                    response.Error = new Error() { ErrorMessage = "There was an error while updating the movie" };
                    response.Status = HttpStatusCode.InternalServerError;
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                response.Error = new Error() { ErrorMessage = "There was some error" };
                response.Status = HttpStatusCode.InternalServerError;
            }

            return response;

        }

        [HttpDelete]
        [Route("DeleteMovie")]
        public async  Task<Response<String>> Delete(int id)
        {

            Response<string> response = new Response<string>();
            try
            {
                Movie? movie = _movieServices.GetMovieById(id);
                
                if(movie == null)
                {
                    response.Status = HttpStatusCode.NotFound;
                    response.Error = new Error() { ErrorMessage = "Movie Dosen't exists" };
                    return response;
                }

                bool result = await _movieServices.DeleteMovie(movie);

                if (result)
                {
                    response.Data = "Movie Deleted successfully";
                    response.Status = HttpStatusCode.OK;
                }
                else
                {
                    response.Error = new Error() { ErrorMessage = "There was an error while deleting the movie" };
                    response.Status = HttpStatusCode.InternalServerError;
                }


            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                response.Error = new Error() { ErrorMessage = "There was some error" };
                response.Status = HttpStatusCode.InternalServerError;
            }


            return response;
        }
    }
}
