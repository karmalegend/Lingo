using System;
using System.Collections.Generic;
using System.Drawing;
using Lingo.Data.Interfaces;
using Lingo.Models;
using Lingo.Services;
using Lingo.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LingoTest.ServicesTests
{
    [TestClass]
    public class GameServiceTest
    {
        private readonly GameService _gameService;
        private readonly Mock<IHighScoreService> _highScoreService = new Mock<IHighScoreService>();
        private readonly Mock<IUserService> _userService = new Mock<IUserService>();
        private readonly Mock<IGameRepo> _gameRepo = new Mock<IGameRepo>();
        private readonly Mock<IWordsRepo> _wordsRepo = new Mock<IWordsRepo>();

        public GameServiceTest()
        {
            _gameService = new GameService(_highScoreService.Object,_userService.Object,_gameRepo.Object,_wordsRepo.Object);
        }

        [TestMethod]
        public void CreateNewGameForUserTestNotFromDb()
        {
            //Using AAA template once to display understanding and usage won't be portrayed this way in future tests.


            //Arrange
            GameSessionModel game = new GameSessionModel() { Guesses = 0 };
            _gameRepo.Setup(repo => repo.GetCurrentGame("username")).Returns((GameSessionModel)null);
            _userService.Setup(service => service.GetUserByUsername("username"))
                .Returns(new UserModel() {Username = "username"});
            _wordsRepo.Setup(repo => repo.GetFiveLetterWord()).Returns(new FiveLetterWordModel() {Word = "about"});
            _gameRepo.Setup(repo => repo.SaveChanges()).Returns(true);

            //Act
            int newGuesses = _gameService.CreateNewGameForUser("user").Guesses;

            //Assert.
            Assert.AreEqual(game.Guesses, newGuesses);

        }


        [TestMethod]
        public void CreateNewGameForUserTestFromDb()
        {
            GameSessionModel game = new GameSessionModel() { Guesses = 0 };
            _gameRepo.Setup(repo => repo.GetCurrentGame("username")).Returns(new GameSessionModel());
            _userService.Setup(service => service.GetUserByUsername("username"))
                .Returns(new UserModel() { Username = "username" });
            _wordsRepo.Setup(repo => repo.GetFiveLetterWord()).Returns(new FiveLetterWordModel() { Word = "about" });
            _gameRepo.Setup(repo => repo.SaveChanges()).Returns(true);

            Assert.AreEqual(game.Guesses, _gameService.CreateNewGameForUser("username").Guesses);
        }

        [TestMethod]
        public void RetrieveGameSessionModelByUsernameTest()
        {
            string username = "username";
            _gameRepo.Setup(repo => repo.GetCurrentGame(username)).Returns(new GameSessionModel());

            Assert.IsInstanceOfType(_gameService.RetrieveGameSessionModelByUsername(username), typeof(GameSessionModel));
        }

        [TestMethod]
        public void CorrectGuessTestInValid()
        {
            Assert.IsFalse(_gameService.CorrectGuess(new List<char>{'A','C','C','C','A'}));
        }

        [TestMethod]
        public void CorrectGuessTestValid()
        {
            Assert.IsTrue(_gameService.CorrectGuess(new List<char> { 'C', 'C', 'C', 'C', 'C' }));
        }

        [TestMethod]
        public void MatchingWordLengthsSameLengths()
        {
            string word = "hello";
            Assert.IsTrue(_gameService.MatchingWordLengths(new GameSessionModel(){Currentword = word }, word ));
        }

        [TestMethod]
        public void MatchingWordLengthsDifferentLengths()
        {
            string word = "word";
            Assert.IsFalse(_gameService.MatchingWordLengths(new GameSessionModel() { Currentword = word }, "woooooooord"));
        }

        [TestMethod]
        public void AttemptGuessTest()
        {
           Assert.IsInstanceOfType(_gameService.AttemptGuess(new GameSessionModel() {Currentword = "word", Guesses = 0}, "word"),typeof(List<List<char>>));
        }

        [TestMethod]
        public void GameOverTestTrue()
        {
            _highScoreService.Setup(hs => hs.AddNewHighScore(new HighScoreModel())).Returns(true);

            Assert.IsTrue(_gameService.GameOver(new GameSessionModel() {Guesses = 5,Score = 5,Player = new UserModel(){Username = "username"}}));
        }

        [TestMethod]
        public void GameOverTestFalse()
        {
            _highScoreService.Setup(hs => hs.AddNewHighScore(new HighScoreModel())).Returns(true);

            Assert.IsFalse(_gameService.GameOver(new GameSessionModel() { Guesses = 3, Score = 5, Player = new UserModel() { Username = "username" } }));
        }

        [TestMethod]
        public void InTimeTestTrue()
        {
            _gameRepo.Setup(repo => repo.SaveChanges()).Returns(true);
            Assert.IsTrue(_gameService.InTime(new GameSessionModel() { LastGuess = DateTime.Now.AddSeconds(9) }));
        }

        [TestMethod]
        public void InTimeTestFalse()
        {
            _gameRepo.Setup(repo => repo.SaveChanges()).Returns(true);
            Assert.IsFalse(_gameService.InTime(new GameSessionModel() { LastGuess = DateTime.Now.AddSeconds(-20) }));
        }

        [TestMethod]
        public void IncrementGuessCounter()
        {
            _gameRepo.Setup(repo => repo.SaveChanges()).Returns(true);
            Assert.IsFalse(_gameService.IncrementGuessCounter(new GameSessionModel() { Guesses = 0 }));
        }

        [TestMethod]
        public void GetNewWordForGameFive()
        {
            _gameRepo.Setup(gr => gr.SaveChanges()).Returns(true);

            _wordsRepo.Setup(wr => wr.GetSixLetterWord()).Returns(new SixLetterWordModel(){Word = "bbbSix"});

            Assert.AreEqual(6,_gameService.GetNewWordForGame(new GameSessionModel(){Currentword = "aFive"}));
        }

        [TestMethod]
        public void GetNewWordForGameSix()
        {
            _gameRepo.Setup(gr => gr.SaveChanges()).Returns(true);
            _wordsRepo.Setup(wr => wr.GetSevenLetterWord()).Returns(new SevenLetterWordModel() {Word = "bbSeven"});

            Assert.AreEqual(7, _gameService.GetNewWordForGame(new GameSessionModel(){Currentword = "aaaSix" }));
        }


        [TestMethod]
        public void GetNewWordForGameSeven()
        {
            _gameRepo.Setup(gr => gr.SaveChanges()).Returns(true);
            _wordsRepo.Setup(wr => wr.GetFiveLetterWord()).Returns(new FiveLetterWordModel() {Word = "bFive"});

            Assert.AreEqual(5, _gameService.GetNewWordForGame(new GameSessionModel(){Currentword = "aaSeven"}));
        }

    }
}
