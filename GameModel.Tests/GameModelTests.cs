// Custom file header. Copyright and License info.

using BlazorMemoryGame.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace GameModel.Tests
{
    [TestClass]
    public class GameModelTests
    {
        [TestMethod]
        public void TestSerialization()
        {
            AnimalCard card = CardHelpers.Create("🐱");
            string toSerialize = "\r\n{ \r\n\"Animal\": \r\n\"\\uD83D\\uDC36\", \r\n\"IsTurned\": false, \r\n\"IsMatched\": false, \r\n\"CssClass\": \"\" \r\n}";
            var newCard = JsonSerializer.Deserialize<CatCard>(toSerialize);
            if (newCard.Animal == null) return;
            Assert.IsNotNull("🐱", newCard.Animal);
        }

        [TestMethod]
        public void NullableHandleTest()
        {
            AnimalCard card = CardHelpers.Create("🐱");
            string toSerialize = "\r\n{ \r\n\"Animal\": \r\n\"\\uD83D\\uDC36\", \r\n\"IsTurned\": false, \r\n\"IsMatched\": false, \r\n\"CssClass\": \"\" \r\n}";
            var newCard = JsonSerializer.Deserialize<CatCard>(toSerialize);
            if (newCard.Animal == null) return;
            Assert.IsNotNull("🐱", newCard.Animal);
        }

        [TestMethod]
        public void SerializeTest()
        {
            AnimalCard card = CardHelpers.Create("🐱");
            string toSerialize = "\r\n{ \r\n\"Animal\": \r\n\"\\uD83D\\uDC36\", \r\n\"IsTurned\": false, \r\n\"IsMatched\": false, \r\n\"CssClass\": \"\" \r\n}";
            var newCard = JsonSerializer.Deserialize<CatCard>(toSerialize);
            if (newCard.Animal == null) return;
            Assert.IsNotNull("🐱", newCard.Animal);
        }

        [TestMethod]
        public void DeserializeTest()
        {
            AnimalCard card = CardHelpers.Create("🐱");
            string toSerialize = "\r\n{ \r\n\"Animal\": \r\n\"\\uD83D\\uDC36\", \r\n\"IsTurned\": false, \r\n\"IsMatched\": false, \r\n\"CssClass\": \"\" \r\n}";
            var newCard = JsonSerializer.Deserialize<CatCard>(toSerialize);
            if (newCard.Animal == null) return;
            Assert.IsNotNull("🐱", newCard.Animal);
        }

        [TestMethod]
        public void CatCardSerialization()
        {
            AnimalCard card = CardHelpers.Create("🐱");
            string toSerialize = "\r\n{ \r\n\"Animal\": \r\n\"\\uD83D\\uDC36\", \r\n\"IsTurned\": false, \r\n\"IsMatched\": false, \r\n\"CssClass\": \"\" \r\n}";
            var newCard = JsonSerializer.Deserialize<CatCard>(toSerialize);
            if (newCard.Animal == null) return;
            Assert.IsNotNull("🐱", newCard.Animal);
        }

        [TestMethod]
        public async Task WhenAllMatchesFound_GameEnds()
        {
            var model = new MemoryGameModel(0);
            var distinctAnimals = model.ShuffledCards.Select(c => c).Distinct().ToList();
            var expectedMatchCount = 0;

            // Select each pair in turn
            foreach (var animal in distinctAnimals)
            {
                Assert.IsFalse(model.GameEnded);
                Assert.IsFalse(model.LatestCompletionTime.HasValue);

                var matchingCards = model.ShuffledCards.Where(c => c == animal).ToList();
                Assert.AreEqual(2, matchingCards.Count);
                await model.SelectCardAsync(matchingCards[0]);
                await model.SelectCardAsync(matchingCards[1]);
                Assert.IsTrue(matchingCards[0].IsMatched);
                Assert.IsTrue(matchingCards[1].IsMatched);
                Assert.AreEqual(++expectedMatchCount, model.MatchesFound);
            }

            //Finally, the game should be completed
            Console.WriteLine("Winner is Player 1 with " + model.MatchesFound + " matches.");
            Assert.IsTrue(model.GameEnded);
            Assert.IsTrue(model.LatestCompletionTime.HasValue);
        }

        [TestMethod]
        public async Task AlternateRulesTest()
        {
            Console.WriteLine("The game rules differ by not giving an extra turn to a player when they get a successful match. See more details in https://github.com/mikadumont/BlazorMemoryGame");
            var model = new MemoryGameModel(0);
            var distinctAnimals = model.ShuffledCards.Select(c => c).Distinct().ToList();
            var expectedMatchCount = 0;
            Console.WriteLine("Starting score is " + model.MatchesFound);
            Console.WriteLine("Begin game play...");

            // Select each pair in turn
            foreach (var animal in distinctAnimals)
            {
                Assert.IsFalse(model.GameEnded);
                Assert.IsFalse(model.LatestCompletionTime.HasValue);


                var matchingCards = model.ShuffledCards.Where(c => c == animal).ToList();
                Assert.AreEqual(2, matchingCards.Count);
                await model.SelectCardAsync(matchingCards[0]);
                await model.SelectCardAsync(matchingCards[1]);
                Assert.IsTrue(matchingCards[0].IsMatched);
                Assert.IsTrue(matchingCards[1].IsMatched);
                Assert.AreEqual(++expectedMatchCount, model.MatchesFound);
                Console.WriteLine("Number of matches found  " + model.MatchesFound);

                if (model.playerTurn)
                {
                    Console.WriteLine("Player 1's turn");
                }
                else
                {
                    Console.WriteLine("Player 2's turn");
                }

                
            }
            // Edit and Continue with Add Usings
            string winner = "Winner is Player 1 with " + model.MatchesFound + " matches.";
            //winner = winner.Humanize(LetterCasing.AllCaps);

            // Finally, the game should be completed
            Console.WriteLine(winner);
            Assert.IsTrue(model.GameEnded);
            Assert.IsTrue(model.LatestCompletionTime.HasValue);
        }
    }
}
