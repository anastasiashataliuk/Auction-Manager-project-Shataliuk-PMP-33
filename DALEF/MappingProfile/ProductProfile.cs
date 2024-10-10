using AutoMapper;
using DALEF.Models;
using DTO;

namespace DALEF.MappingProfile
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<TblProduct, Product>();
            CreateMap<Product, TblProduct>();
        }
    }
}
