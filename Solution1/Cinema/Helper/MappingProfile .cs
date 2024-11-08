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
            CreateMap<Show, ShowDTO>();
            CreateMap<ShowDTO, Show>();
            CreateMap<Room, RoomDTO>();
            CreateMap<RoomDTO, Room>();
            CreateMap<Film, FilmDTO>();
            CreateMap<FilmDTO, Film>();
        }   
    }
}
