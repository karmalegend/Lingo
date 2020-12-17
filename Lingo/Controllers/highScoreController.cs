using AutoMapper;
using Lingo.DTO;
using Lingo.Models;
using Lingo.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Lingo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HighScoreController : ControllerBase
    {
        private readonly IHighScoreService _highScoreService;
        private readonly IMapper _mapper;

        public HighScoreController(IHighScoreService highScoreService, IMapper mapper) {
            _highScoreService = highScoreService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{top}")]
        public IActionResult GetHighScores(int top) {
            if (top < 0)
            {
                return BadRequest();
            }
            List<HighScoreDtoRead> highscores = new List<HighScoreDtoRead>();
            foreach (HighScoreModel highscore in _highScoreService.GetHighScores(top)) {
                if (highscore != null) {
                    highscores.Add(_mapper.Map<HighScoreDtoRead>(highscore));
                }
            }
            
            return Ok(highscores);
        }
    }
}
