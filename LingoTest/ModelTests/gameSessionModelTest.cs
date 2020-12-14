using Lingo.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LingoTest.ModelTests
{
    [TestClass]
    public class gameSessionModelTest
    {
        private gameSessionModel initObject() {
            gameSessionModel model = new gameSessionModel();
            model.Guesses = 0;
            model.lastGuess = DateTime.Now;
            return model;
        }

        [TestMethod]
        public void increaseGuessTest() {
            gameSessionModel model = initObject();
            model.increaseGuess();
            Assert.AreEqual(model.Guesses, 1);
        }

        [TestMethod]
        public void guessedIntimeTestIntime() {
            gameSessionModel model = initObject();
            Assert.IsTrue(model.guessedIntime(model.lastGuess.AddSeconds(5)));
        }

        [TestMethod]
        public void guessedIntimeTestOutOfTime() {
            gameSessionModel model = initObject();
            Assert.IsFalse(model.guessedIntime(model.lastGuess.AddSeconds(11)));
        }
    }
}
