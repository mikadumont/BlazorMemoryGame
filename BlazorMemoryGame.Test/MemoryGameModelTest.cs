// Custom file header. Copyright and License info.

using BlazorMemoryGame.Models;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace BlazorMemoryGame.Test
{
    public class MemoryGameModelTest
    {
        [Fact]
        public void StartsWithZeroMatches()
        {
            var model = new MemoryGameModel(0);
            Assert.Equal(0, model.MatchesFound);
        }

        [Fact]
        public void StartsWithAllCardsFaceDown()
        {
            var model = new MemoryGameModel(0);
            Assert.All(model.ShuffledCards, card => Assert.False(card.IsTurned));
        }

        [Fact]
        public async Task WhenUserSelectsNonMatchingPair_TurnsBothBack()
        {
            // Find a non-matching pair
            var model = new MemoryGameModel(0);
            var firstSelection = model.ShuffledCards[0];
            var secondSelection = model.ShuffledCards
                .First(c => c != firstSelection);

            // Select first one
            await model.SelectCardAsync(firstSelection);
            Assert.True(firstSelection.IsTurned);

            // Select second one - everything resets
            await model.SelectCardAsync(secondSelection);
            Assert.False(firstSelection.IsTurned);
            Assert.False(secondSelection.IsTurned);
            Assert.False(firstSelection.IsMatched);
            Assert.False(firstSelection.IsMatched);
            Assert.Equal(0, model.MatchesFound);
        }

        [Fact]
        public async Task WhenUserSelectsMatchingPair_StaysMatched()
        {
            // Find a non-matching pair
            var model = new MemoryGameModel(0);
            var firstSelection = model.ShuffledCards[0];
            var secondSelection = model.ShuffledCards.Skip(1)
                .Single(c => c == firstSelection);

            // Select first one
            await model.SelectCardAsync(firstSelection);
            Assert.True(firstSelection.IsTurned);
            Assert.Equal(0, model.MatchesFound);

            // Select second one - everything resets
            await model.SelectCardAsync(secondSelection);
            Assert.True(firstSelection.IsTurned);
            Assert.True(secondSelection.IsTurned);
            Assert.True(firstSelection.IsMatched);
            Assert.True(secondSelection.IsMatched);
            Assert.Equal(1, model.MatchesFound);
            Assert.False(model.GameEnded);
        }

        [Fact]
        public async Task WhenAllMatchesFound_GameEnds()
        {
            var model = new MemoryGameModel(0);
            var distinctAnimals = model.ShuffledCards.Select(c => c).Distinct().ToList();
            var expectedMatchCount = 0;

            // Select each pair in turn
            foreach (var animal in distinctAnimals)
            {
                Assert.False(model.GameEnded);
                Assert.False(model.LatestCompletionTime.HasValue);

                var matchingCards = model.ShuffledCards.Where(c => c == animal).ToList();
                Assert.Equal(2, matchingCards.Count);
                await model.SelectCardAsync(matchingCards[0]);
                await model.SelectCardAsync(matchingCards[1]);
                Assert.True(matchingCards[0].IsMatched);
                Assert.True(matchingCards[1].IsMatched);
                Assert.Equal(++expectedMatchCount, model.MatchesFound);
            }

            // Finally, the game should be completed
            Assert.True(model.GameEnded);
            Assert.True(model.LatestCompletionTime.HasValue);
        }

        [Fact]
        public void TestSerialization()
        {
            AnimalCard card = AnimalCard.Create("🐶");
            var jsonString = JsonSerializer.Serialize<DogCard>((DogCard)card);
            Assert.Equal("{\"Animal\":\"\\uD83D\\uDC36\",\"IsTurned\":false,\"IsMatched\":false,\"CssClass\":\"\"}", jsonString);

            // Convert regular string literal to verbatim string literal
            string toSerialize = "\r\n{ \r\n\"Animal\": \r\n\"\\uD83D\\uDC36\", \r\n\"IsTurned\": false, \r\n\"IsMatched\": false, \r\n\"CssClass\": \"\" \r\n}";
            var newCard = JsonSerializer.Deserialize<DogCard>(toSerialize);
            Assert.Equal("🐶", newCard.Animal);
        }
    }
}