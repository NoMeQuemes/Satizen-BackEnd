using AutoMapper;
using Proyec_Satizen_Api.Models.Dto;
using Satizen_Api.Models;

namespace Proyec_Satizen_Api
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Institucion, InstitucionDto>();
            CreateMap<InstitucionDto, Institucion>();

            CreateMap<Institucion, InstitucionCreateDto>().ReverseMap();
            CreateMap<Institucion, InstitucionUpdateDto>().ReverseMap();

        }
    }
}
