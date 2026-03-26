
using AutoMapper;
using Shoping_Karo.DTOs;
using Shoping_Karo.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace login_REg.Server.Mapper
{
    public class autoMapper : Profile
    {
        public autoMapper()
        { 
            CreateMap<LoginDto, users>().ReverseMap();
        }
    }
}
