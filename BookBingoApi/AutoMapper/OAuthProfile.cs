using AutoMapper;
using BookBingoApi.Models;
using BookBingoApi.Repository.Cosmos.Daos;

namespace BookBingoApi.AutoMapper
{
    public class OAuthProfile : Profile
    {
        public OAuthProfile()
        {
            CreateMap<OAuthToken, OAuthTokenDao>();
            CreateMap<OAuthTokenDao, OAuthToken>();
        }
    }
}
