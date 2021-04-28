using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace BlazorMemoryGame.Models
{
    /// <summary>
    /// <c>AnimalCard</c> displays a unique animal on each card
    /// <para>
    /// <see cref="IEquatable{T}"/>
    /// </para>
    /// <para><see href="https://home.unicode.org/">type windows key + period for emoji characters on Windows</see></para>
    /// <list type="bullet">
    /// <itm><description><para><em>animal card</em></para></description></itm>
    /// </list>
    /// <list type="number">
    /// <item><description><para><strong>animal card</strong></para></description></item>
    /// </list>
    /// </summary>
    public abstract class AnimalCard : IEquatable<AnimalCard>
    {
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