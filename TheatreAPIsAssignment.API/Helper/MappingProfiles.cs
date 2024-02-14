using AutoMapper;
using TheatreAPIsAssignment.Contracts.Dto;
using TheatreAPIsAssignment.Contracts.Models;

namespace TheatreAPIsAssignment.API.Helper
{

        public class MappingProfiles : Profile
        {
            public MappingProfiles()
            {
                CreateMap<MovieDto, Movie>()
                                .ForMember(dest => dest.Shows, opt => opt.Ignore()); 

            CreateMap<ShowDto, Show>();

            CreateMap<Movie, MovieDto>();
           
            CreateMap<Show, ShowDto>();
        }
        }
    
}
