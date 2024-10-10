using AutoMapper;
using DALEF.Models;
using DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DALEF.MappingProfile
{
    public class AuctionProfile : Profile
    {
        public AuctionProfile()
        {
            CreateMap<TblAuction, Auction>();
            CreateMap<Auction, TblAuction>();
        }
    }
}