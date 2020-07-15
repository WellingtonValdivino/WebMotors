using AutoMapper;
using WebMotors.Domain;
using WebMotors.WebAPI.Dtos;

namespace WebMotors.WebAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Anuncio, AnuncioDto>().ReverseMap();
        }
    }
}