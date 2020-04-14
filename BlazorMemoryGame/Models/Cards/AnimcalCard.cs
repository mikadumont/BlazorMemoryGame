using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace BlazorMemoryGame.Models
{
    public abstract class AnimalCard : IEquatable<AnimalCard>
    {
        public static AnimalCard Create (string animal)
        {
            return animal switch
            {
                "🐶" => new DogCard(),
                "🐺" => new WolfCard(),
                "🐮" => new OxCard(),
                "🦊" => new FoxCard(),
                "🐱" => new CatCard(),
                "🦁" => new LionCard(),
                "🐯" => new PatherCard(),
                "🐹" => new MouseCard(),
                _ => throw new ArgumentException(nameof(animal)),
            };
        }

        /// <summary>
        /// The emoji that we display on the card face
        /// </summary>
        public abstract string Animal { get; }

        public bool IsTurned { get; set; }
        public bool IsMatched { get; set; }

        public string CssClass
        {
            get
            {
                switch ((IsTurned, IsMatched))
                {
                    case (false, true): return "matched";
                    case (true, false): return "turned";
                    case (true, true): return "turned matched";
                    default: return "";
                }
            }
        }

        public bool Equals(AnimalCard other)
            => string.CompareOrdinal(Animal, other.Animal) == 0;

        public override int GetHashCode()
            => HashCode.Combine(Animal);

        public static bool operator ==(AnimalCard left, AnimalCard right)
            => EqualityComparer<AnimalCard>.Default.Equals(left, right);

        public static bool operator !=(AnimalCard left, AnimalCard right)
            => !(left == right);

        public override bool Equals(object obj)
            => obj is AnimalCard animal && this == animal;
    }
}
