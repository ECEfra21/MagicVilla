using AutoMapper;
using MagicVilla_API.Modelos;
using MagicVilla_API.Modelos.Dto;

namespace MagicVilla_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            // origen, destino
            CreateMap<Villa, PVilla>();
            CreateMap<Villa, PVilla>().ReverseMap();
            
            CreateMap<NumeroVilla, PNumeroVilla>();
            CreateMap<NumeroVilla, PNumeroVilla>().ReverseMap();
        }
    }
}
