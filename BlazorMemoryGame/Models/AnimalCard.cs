// Custom file header. Copyright and License info.

using System;
using System.Collections.Generic;

namespace BlazorMemoryGame.Models
{
    /// <summary>
    /// <c>AnimalCard</c> displays a unique animal on each card
    /// <para>
    /// <see cref="IEquatable{T}"/>
    /// </para>
    /// <see href="https://support.apple.com/en-us/HT201586">see how to type emoji characters on Mac</see>
    /// <para><see href="https://home.unicode.org/">type windows key + period for emoji characters on Windows</see></para>
    /// <list type="bullet">
    /// <itm><description><para><em>animal card</em></para></description></itm>
    /// </list>
    /// <list type="number">
    /// <item><description><para><strong>animal card</strong></para></description></item>
    /// </list>
    /// </summary>
    public abstract partial class AnimalCard : IEquatable<AnimalCard>
    {
        public static AnimalCard Create(string animal)
        {
            return NewMethod(animal);

            static AnimalCard NewMethod(string animal)
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

        public abstract string Animal { get; }

        public bool IsTurned { get; set; }
        public bool IsMatched { get; set; }

        public string CssClass
        {
            get
            {
                //Convert switch statement to switch expression
                return (IsTurned, IsMatched) switch
                {
                    (false, true) => "matched",
                    (true, false) => "turned",
                    (true, true) => "turned matched",
                    _ => "",
                };
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
