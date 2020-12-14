using AutoMapper;
using Lingo.DTO;
using Lingo.Models;

namespace Lingo.Profiles
{
    public class HighScoreProfile : Profile
    {
        public HighScoreProfile()
        {
            CreateMap<HighScoreModel, HighScoreDtoRead>();
        }
    }
}
