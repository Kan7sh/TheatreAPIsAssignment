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
    public class ShowController : ControllerBase
    {
        private readonly IShowServices _showServices;
        private readonly IMovieServices _movieServices;
        private readonly IMapper _mapper;

        public ShowController(IShowServices showServices,IMovieServices movieServices,IMapper mapper)
        {
            _showServices = showServices;
            _movieServices = movieServices;
            _mapper = mapper;

        }

        [HttpPost]
        [Route("AddShow")]
        public async Task<Response<string>> PostShow([FromBody] ShowDto showDto)
        {

            Response<string> response = new Response<string>();
            try
            {

                string? validateMessage = _showServices.CheckForEmptyProperties(showDto);

                if (validateMessage != null)
                {
                    response.Status = HttpStatusCode.BadRequest;
                    response.Error = new Error() { ErrorMessage = validateMessage };
                    return response;
                }

                if (!_showServices.IsValidTimings(showDto.Time))
                {

                    response.Status = HttpStatusCode.BadRequest;
                    response.Error = new Error() { ErrorMessage = "Invalid show timings" };
                    return response;
                }

                Movie? movie = _movieServices.GetMovieById(showDto.MovieId);

      

                if (movie == null)
                {
                    
                    response.Status = HttpStatusCode.NotFound;
                    response.Error = new Error() { ErrorMessage = "Movie Dosen't exists" };
                    return response;
                }



                var showMap = _mapper.Map<Show>(showDto);

                showMap.Movie = movie;

                bool showExists = await _showServices.CheckShowExists(showMap);

                if (showExists)
                {
                    response.Status = HttpStatusCode.Conflict;
                    response.Error = new Error() { ErrorMessage = "Show already exists" };
                    return response;

                }

                bool result = await _showServices.AddShow(showMap);

                if (result)
                {
                    response.Data = "Show added successfully";
                    response.Status = HttpStatusCode.OK;
                }
                else
                {
                    response.Error = new Error() { ErrorMessage = "There was some error while adding the show" };
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

        [HttpPut()]
        [Route("BookTickets")]
        public async Task<Response<string>> UpdateShow(int id, int seats)
        {
            Response<string> response = new Response<string>();
            try
            {
                Show? show = await _showServices.GetShowById(id);

                if(show == null)
                {
                    response.Status = HttpStatusCode.NotFound;
                    response.Error = new Error() { ErrorMessage = "Show Dosen't exists" };
                    return response;
                }

                show.BookedSeats += seats;

                if(show.BookedSeats > 100)
                {
                    response.Status = HttpStatusCode.Conflict;
                    response.Error = new Error() { ErrorMessage = "No enough seats available" };
                    return response;
                }

                bool result = await _showServices.UpdateShow(show);

                if (result)
                {
                    response.Data = "Seats updated successfully";
                    response.Status = HttpStatusCode.OK;
                }
                else
                {
                    response.Error = new Error() { ErrorMessage = "There was some error while updating the data" };
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
