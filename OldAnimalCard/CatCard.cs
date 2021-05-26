// Custom file header. Copyright and License info.

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OldAnimalCard
{
    public abstract class CatCard : IEquatable<CatCard>
    {
        public Regex emojiString;
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

        public bool Equals(CatCard other)
            => string.CompareOrdinal(Animal, other.Animal) == 0;

        public override int GetHashCode()
            => HashCode.Combine(Animal);

        public static bool operator ==(CatCard left, CatCard right)
            => EqualityComparer<CatCard>.Default.Equals(left, right);

        public static bool operator !=(CatCard left, CatCard right)
            => !(left == right);

        public override bool Equals(object obj)
            => obj is CatCard animal && this == animal;
    }
}
