using AutoMapper;
using Lingo.DTO;
using Lingo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingo.Profiles
{
    public class highScoreProfile : Profile
    {
        public highScoreProfile()
        {
            CreateMap<highScoreModel, highScoreDtoRead>();
        }
    }
}
