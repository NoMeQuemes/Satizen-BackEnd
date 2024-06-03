using AutoMapper;
using Proyec_Satizen_Api.Models;
using Proyec_Satizen_Api.Models.Dto;

namespace Proyec_Satizen_Api
{
    public class MappingConfig : Profile 
    {
        public MappingConfig()
        { 
            CreateMap<InstitucionModels, InstitucionDto>();
            CreateMap<InstitucionDto,InstitucionModels>();

            CreateMap<InstitucionModels, InstitucionCreateDto>().ReverseMap();
            CreateMap<InstitucionModels, InstitucionUpdateDto>().ReverseMap();

        }
    }
}
