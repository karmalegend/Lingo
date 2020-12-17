using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Lingo.Controllers;
using Lingo.DTO;
using Lingo.Models;
using Lingo.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LingoTest.ControllerTests
{
    [TestClass]
    public class HighScoreControllerTest
    {
        private readonly HighScoreController _highScoreController;
        private readonly Mock<IHighScoreService> _highScoreService = new Mock<IHighScoreService>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();

        public HighScoreControllerTest()
        {
            _highScoreController = new HighScoreController(_highScoreService.Object, _mapper.Object);
        }

        [TestMethod]
        public void GetHighScoresTest()
        {
            
            int value = 10;
            List<HighScoreModel> highscores = new List<HighScoreModel>();

            for (int i = 0; i < value; i++)
            {
                highscores.Add(new HighScoreModel());
            }

            _highScoreService.Setup(hs => hs.GetHighScores(value)).Returns(highscores);

            OkObjectResult result  = _highScoreController.GetHighScores(value) as OkObjectResult;
            List<HighScoreDtoRead> resultBody = (List<HighScoreDtoRead>) result.Value;


            Assert.IsInstanceOfType(result,typeof(OkObjectResult));
            Assert.AreEqual(value, resultBody.Count);
        }


        [TestMethod]
        public void GetHighScoresTestInvalidNumberTest()
        {
            BadRequestResult result = _highScoreController.GetHighScores(-1) as BadRequestResult;

            Assert.IsInstanceOfType(result,typeof(BadRequestResult));
        }
    }
}
