using System.Collections.Generic;
using Lingo.Data.Interfaces;
using Lingo.Models;
using Lingo.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LingoTest.ServicesTests
{
    [TestClass]
    public class HighScoreServiceTest
    {
        private readonly HighScoreService _highScoreService;
        private readonly Mock<IHighscoreRepo> _highScoreRepo = new Mock<IHighscoreRepo>();

        public HighScoreServiceTest()
        {
            _highScoreService = new HighScoreService(_highScoreRepo.Object);
        }

        [TestMethod]
        public void AddNewHighScoreTest()
        {
            _highScoreRepo.Setup(hr => hr.GetHighScoreModel(new HighScoreModel())).Returns((HighScoreModel) null);
            _highScoreRepo.Setup(hr => hr.SaveChanges()).Returns(true);

            Assert.IsTrue(_highScoreService.AddNewHighScore(new HighScoreModel()));
        }


        [TestMethod]
        public void GetHighScoresTest()
        {
            int top = 10;
            _highScoreRepo.Setup(hr => hr.GetTopHighscores(top)).Returns(new List<HighScoreModel>());

            Assert.IsInstanceOfType(_highScoreService.GetHighScores(top),typeof(List<HighScoreModel>));
        }
    }
}
