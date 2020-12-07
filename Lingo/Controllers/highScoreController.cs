using AutoMapper;
using Lingo.DTO;
using Lingo.Models;
using Lingo.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class highScoreController : ControllerBase
    {
        private readonly IHighScoreService _highScoreService;
        private readonly IMapper _mapper;

        public highScoreController(IHighScoreService highScoreService, IMapper mapper) {
            _highScoreService = highScoreService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{top}")]
        public IActionResult getHighScores(int top) {
            List<highScoreDtoRead> highscores = new List<highScoreDtoRead>();
            foreach (highScoreModel highscore in _highScoreService.getHighScores(top)) {
                if (highscore != null) {
                    highscores.Add(_mapper.Map<highScoreDtoRead>(highscore));
                }
            }
            
            return Ok(highscores);
        }
    }
}
