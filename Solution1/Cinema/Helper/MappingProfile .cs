using AutoMapper;
using Cinema.Models;
using Share.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Ánh xạ từ Show sang ShowDTO
            CreateMap<Show, ShowDTO>()
                .ForMember(dest => dest.FilmTitle, opt => opt.MapFrom(src => src.Film.Title))// Ánh xạ Film.Title
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room.Name));// Ánh xạ Film.Title
            CreateMap<ShowDTO, Show>();
            CreateMap<Room, RoomDTO>();
            CreateMap<RoomDTO, Room>();
            CreateMap<Film, FilmDTO>();
            CreateMap<FilmDTO, Film>();
        }   
    }
}
