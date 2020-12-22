using System;
using System.Collections.Generic;
using AutoMapper;
using Lingo.Controllers;
using Lingo.DTO;
using Lingo.Models;
using Lingo.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LingoTest.ControllerTests
{
    [TestClass]
    public class GameControllerTest
    {
        private readonly GameController _gameController;
        private readonly Mock<IGameService> _gameService = new Mock<IGameService>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private readonly Mock<ControllerContext> _context = new Mock<ControllerContext>();

        public GameControllerTest()
        {
            _gameController = new GameController(_gameService.Object, _mapper.Object);
        }

        [TestMethod]
        public void CreateNewGameSessionTest()
        {
            string username = "username";
            
            //https://rovani.net/Mock-Controller-Url/
            //literally the only solution that worked after hours of stack overflow.
            
            var mockContext = new Mock<HttpContext>(MockBehavior.Strict);
            mockContext.SetupGet(hc => hc.User.Identity.Name).Returns(username);
            _gameController.ControllerContext = new ControllerContext()
            {
                HttpContext = mockContext.Object
            };


            _gameService.Setup(gs => gs.CreateNewGameForUser(username)).Returns(new GameSessionModel());
            
            Assert.IsInstanceOfType(_gameController.CreateNewGameSession(),typeof(OkObjectResult));
        }


        [TestMethod]
        public void CreateNewGameSessionTestBadRequest()
        {
            string username = "username";

            var mockContext = new Mock<HttpContext>(MockBehavior.Strict);
            mockContext.SetupGet(hc => hc.User.Identity.Name).Returns(username);
            _gameController.ControllerContext = new ControllerContext()
            {
                HttpContext = mockContext.Object
            };


            _gameService.Setup(gs => gs.CreateNewGameForUser(username)).Returns((GameSessionModel) null);

            Assert.IsInstanceOfType(_gameController.CreateNewGameSession(), typeof(BadRequestResult));
        }

        [TestMethod]
        public void GuessWordHappyTest()
        {
            //prep auth
            string username = "username";

            GameSessionModel gameSession = new GameSessionModel()
                {Currentword = "word", Guesses = 3, LastGuess = DateTime.Now, Player = new UserModel(), Score = 5};

            GuessWriteDto guessWrite = new GuessWriteDto() {Guess = "word"};

            int newWordLength = 5;
            

            var mockContext = new Mock<HttpContext>(MockBehavior.Strict);
            mockContext.SetupGet(hc => hc.User.Identity.Name).Returns(username);
            _gameController.ControllerContext = new ControllerContext()
            {
                HttpContext = mockContext.Object
            };

            _gameService.Setup(gs => gs.RetrieveGameSessionModelByUsername(username)).Returns(gameSession);
            _gameService.Setup(gs => gs.GameOver(gameSession)).Returns(false);
            _gameService.Setup(gs => gs.InTime(gameSession)).Returns(true);
            _gameService.Setup(gs => gs.MatchingWordLengths(gameSession, "word")).Returns(true);
            _gameService.Setup(gs => gs.AttemptGuess(gameSession, guessWrite.Guess)).Returns(new List<List<char>>(){new List<char>(){'a','a'},new List<char>(){'a','a'}});
            _gameService.Setup(gs => gs.CorrectGuess(new List<char>(){'a','a'})).Returns(true);
            _gameService.Setup(gs => gs.GetNewWordForGame(gameSession)).Returns(newWordLength);

            OkObjectResult result = _gameController.GuessWord(guessWrite) as OkObjectResult;


            Assert.IsInstanceOfType(result,typeof(OkObjectResult));
            Assert.AreEqual($"Congratulations you've correctly guessed the word! a new {newWordLength} letter word has been selected.", result.Value);
        }

        [TestMethod]
        public void GuessWordNoGame()
        {
            //prep auth
            string username = "username";

            var mockContext = new Mock<HttpContext>(MockBehavior.Strict);
            mockContext.SetupGet(hc => hc.User.Identity.Name).Returns(username);
            _gameController.ControllerContext = new ControllerContext()
            {
                HttpContext = mockContext.Object
            };

            ConflictObjectResult result = _gameController.GuessWord(new GuessWriteDto()) as ConflictObjectResult;
            Assert.IsInstanceOfType(result, typeof(ConflictObjectResult));
            Assert.AreEqual("User doesn't currently have any game.", result.Value );

        }

        [TestMethod]
        public void GuessWordGameOver()
        {
            //prep auth
            string username = "username";
            GameSessionModel gameSessionModel = new GameSessionModel() {Guesses = 5, Score = 10};

            var mockContext = new Mock<HttpContext>(MockBehavior.Strict);
            mockContext.SetupGet(hc => hc.User.Identity.Name).Returns(username);
            _gameController.ControllerContext = new ControllerContext()
            {
                HttpContext = mockContext.Object
            };

            _gameService.Setup(gs => gs.RetrieveGameSessionModelByUsername(username))
                .Returns(gameSessionModel);
            _gameService.Setup(gs => gs.GameOver(gameSessionModel)).Returns(true);

            OkObjectResult result =  _gameController.GuessWord(new GuessWriteDto()) as OkObjectResult;


            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual("You've reached the maximum ammount of guesses please create a new game and try again." + $"\n You're score this game was {gameSessionModel.Score}", result.Value);
        }
    }
}
