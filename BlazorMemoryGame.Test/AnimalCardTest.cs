// Custom file header. Copyright and License info.

using BlazorMemoryGame.Models;
using System.Text.Json;
using Xunit;

namespace BlazorMemoryGame.Test
{
    public class AnimalCardTest
    {
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

            AnimalCard ac = (AnimalCard)JsonSerializer.Deserialize(jsonString, typeof(DogCard));
            Assert.Equal("🐶", ac.Animal);
        }

        [Fact]
        public void CardSerializationTest()
        {
            AnimalCard card = AnimalCard.Create("🐶");
            var jsonString = JsonSerializer.Serialize<DogCard>((DogCard)card);
            Assert.Equal("{\"Animal\":\"\\uD83D\\uDC36\",\"IsTurned\":false,\"IsMatched\":false,\"CssClass\":\"\"}", jsonString);

            string toSerialize = "\r\n{ \r\n\"Animal\": \r\n\"\\uD83D\\uDC36\", \r\n\"IsTurned\": false, \r\n\"IsMatched\": false, \r\n\"CssClass\": \"\" \r\n}";
            var newCard = JsonSerializer.Deserialize<DogCard>(toSerialize);
            Assert.Equal("🐶", newCard.Animal);
        }

        [Fact]
        [Trait("Category", "AnimalCard")]
        public void CardSerializationTest2()
        {
            AnimalCard card = AnimalCard.Create("🐶");
            var jsonString = JsonSerializer.Serialize<DogCard>((DogCard)card);
            Assert.Equal("{\"Animal\":\"\\uD83D\\uDC36\",\"IsTurned\":false,\"IsMatched\":false,\"CssClass\":\"\"}", jsonString);

            string toSerialize = "\r\n{ \r\n\"Animal\": \r\n\"\\uD83D\\uDC36\", \r\n\"IsTurned\": false, \r\n\"IsMatched\": false, \r\n\"CssClass\": \"\" \r\n}";
            var newCard = JsonSerializer.Deserialize<DogCard>(toSerialize);
            Assert.Equal("🐶", newCard.Animal);

        }

        [Fact]
        [Trait("Category", "AnimalCard")]
        public void DogMatchDeserializaionTest()
        {
            AnimalCard card = AnimalCard.Create("🐶");
            var jsonString = JsonSerializer.Serialize<DogCard>((DogCard)card);
            Assert.Equal("{\"Animal\":\"\\uD83D\\uDC36\",\"IsTurned\":false,\"IsMatched\":false,\"CssClass\":\"\"}", jsonString);

            string toSerialize = "\r\n{ \r\n\"Animal\": \r\n\"\\uD83D\\uDC36\", \r\n\"IsTurned\": false, \r\n\"IsMatched\": false, \r\n\"CssClass\": \"\" \r\n}";
            var newCard = JsonSerializer.Deserialize<DogCard>(toSerialize);

            Assert.Equal("🐶", newCard.Animal);

        }

        [Fact]
        [Trait("Category", "AnimalCard")]
        public void NewTest()
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
