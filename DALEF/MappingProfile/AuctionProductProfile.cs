using AutoMapper;
using DALEF.Models;
using DTO;

namespace DALEF.MappingProfile
{
    public class AuctionProductProfile : Profile
    {
        public AuctionProductProfile()
        {
            CreateMap<TblAuctionProduct, AuctionProduct>();
            CreateMap<AuctionProduct, TblAuctionProduct>();
        }
    }
}
