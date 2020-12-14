using Lingo.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace LingoTest.ModelTests
{
    [TestClass]
    public class GuessVerifierTest
    {
        [TestMethod]
        public void testCorrectWordCorrect() {
            string word = "hello";
            List<List<char>> results = GuessVerifier.CheckResult(word,word);

            int correct = 0;

            foreach (char x in results[1]) {
                if (x.Equals('C')) {
                    correct++;
                }
            }

            Assert.AreEqual(correct, word.Length);

        }

        [TestMethod]
        public void TestCorrectWordIncorrect() {
            string word = "hilol";
            List<List<char>> results = GuessVerifier.CheckResult(word, "hillo");

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
        public void TestCorrectWordInvalidLengths() {
            Assert.ThrowsException<IndexOutOfRangeException>(() =>
                 GuessVerifier.CheckResult("word", "wo")
            );
        }
    }
}
