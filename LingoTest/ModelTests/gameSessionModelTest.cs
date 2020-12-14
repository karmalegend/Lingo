using Lingo.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LingoTest.ModelTests
{
    [TestClass]
    public class GameSessionModelTest
    {
        private GameSessionModel initObject() {
            GameSessionModel model = new GameSessionModel();
            model.Guesses = 0;
            model.LastGuess = DateTime.Now;
            return model;
        }

        [TestMethod]
        public void IncreaseGuessTest() {
            GameSessionModel model = initObject();
            model.IncreaseGuess();
            Assert.AreEqual(model.Guesses, 1);
        }

        [TestMethod]
        public void GuessedIntimeTestIntime() {
            GameSessionModel model = initObject();
            Assert.IsTrue(model.GuessedIntime(model.LastGuess.AddSeconds(5)));
        }

        [TestMethod]
        public void GuessedIntimeTestOutOfTime() {
            GameSessionModel model = initObject();
            Assert.IsFalse(model.GuessedIntime(model.LastGuess.AddSeconds(11)));
        }
    }
}
