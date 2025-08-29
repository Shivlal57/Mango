using AutoMapper;
using Mango.Service.ProductAPI.Models;
using Mango.Service.ProductAPI.Models.Dto;

namespace Mango.Services.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDto>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
