// Custom file header. Copyright and License info.

using System;

namespace GameModel.Tests
{
    [TestClass]
    public class GameModelTests
    {
        [TestMethod]
        public void WhenAllMatchesFoundGameEnds()
        {
            var model = new MemoryGameModel("@dcampbell", "Dustin Campbell");

            if (dustin.Description == null) return;
            int length = dustin.Description.Length;
            Assert.IsNotNull(length);
        }

        [TestMethod]
        public void RangesAndIndicesTest()
        {
            PowerTweeter dustin = new PowerTweeter("Dustin", "Campbell");
            dustin.FavoriteTopics = new string[]
            {
                // index from start     index from end
                "Star Wars", // 0                    ^4
                "Rock and Roll", // 1                    ^3
                "C# things", // 2                    ^2
                "Doctor Who" // 3                    ^1
            };

            Print("Last favorite:", dustin.FavoriteTopics[^1]);
            Print("Last favorite:", dustin.FavoriteTopics[dustin.FavoriteTopics.Length - 1]);

            Print("Top 3 favorites:", dustin.FavoriteTopics[..3]);
            Print("Also top 3 favorites:", dustin.FavoriteTopics[..^1]);
        }

        private static void Print(string a, string[] favorites)
        {
            Console.WriteLine("Hello");
            foreach (string s in favorites) { Console.WriteLine(s); }
        }

        private static void Print(string a, string s)
        {
            Console.WriteLine(a + "\n" + s);
        }

        [TestMethod]
        public void NullableHandleTest()
        {
            PowerTweeter dustin = new PowerTweeter("@dcampbell", "Dustin Campbell");

            if (dustin.TwitterHandle == null) return;
            int length = dustin.TwitterHandle.Length;
            Assert.IsNotNull(length);
        }

        [TestMethod]
        public void NullableDisplayNameTest()
        {
            PowerTweeter dustin = new PowerTweeter("@dcampbell", "Dustin Campbell");

            if (dustin.TwitterDisplayName == null) return;
            int length = dustin.TwitterDisplayName.Length;
            Assert.IsNotNull(length);
        }

        [TestMethod]
        public void NullableFavoriteTopicsTest()
        {
            PowerTweeter dustin = new PowerTweeter("@dcampbell", "Dustin Campbell");

            if (dustin.FavoriteTopics is null) return;
            int length = dustin.FavoriteTopics.Length;
            Assert.IsNotNull(length);
        }
    }
}