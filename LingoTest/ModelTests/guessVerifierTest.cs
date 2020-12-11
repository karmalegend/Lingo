using Lingo.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LingoTest.ModelTests
{
    [TestClass]
    public class guessVerifierTest
    {
        [TestMethod]
        public void testCorrectWordCorrect() {
            string word = "hello";
            List<List<char>> results = guessVerifier.checkResult(word,word);

            int correct = 0;

            foreach (char x in results[1]) {
                if (x.Equals('C')) {
                    correct++;
                }
            }

            Assert.AreEqual(correct, word.Length);

        }

        [TestMethod]
        public void testCorrectWordIncorrect() {
            string word = "hilol";
            List<List<char>> results = guessVerifier.checkResult(word, "hillo");

            int correct = 0;
            int present = 0;

            foreach (char x in results[1])
            {
                if (x.Equals('C'))
                {
                    correct++;
                }
                else if (x.Equals('P')) {
                    present++;
                }
            }

            Assert.AreEqual(correct, word.Length-2);
            Assert.AreEqual(present, 2);
        }

        [TestMethod]
        public void testCorrectWordInvalidLengths() {
            Assert.ThrowsException<IndexOutOfRangeException>(() =>
                 guessVerifier.checkResult("word", "wo")
            ) ;
        }
    }
}
