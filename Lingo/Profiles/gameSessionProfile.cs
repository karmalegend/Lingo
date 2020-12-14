using AutoMapper;
using Lingo.DTO;
using Lingo.Models;

namespace Lingo.Profiles
{
    public class GameSessionProfile : Profile
    {
        public GameSessionProfile() {
            CreateMap<GameSessionModel, GameSessionDtoRead>();
        }
    }
}
