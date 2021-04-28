using System;
using System.Collections.Immutable;

namespace BlazorMemoryGame.Models
{
    public static class CardHelpers
    {
        public static ImmutableArray<string> AllAnimalEmojis { get; } = (new[] { "🐶", "🐺", "🐮", "🦊", "🐱", "🦁", "🐯", "🐹" }).ToImmutableArray();

        public static AnimalCard Create(string animal)
        {
            return animal switch
            {

                "🐶" => new DogCard(),
                "🐺" => new WolfCard(),
                "🐮" => new OxCard(),
                "🦊" => new FoxCard(),
                "🐱" => new CatCard(),
                "🦁" => new LionCard(),
                "🐯" => new TigerCard(),
                "🐹" => new MouseCard(),
                _ => throw new ArgumentException(nameof(animal)),
            };
        }
    }
}