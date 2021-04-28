using System;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace BlazorMemoryGame.SourceGenerator
{
    [Generator]
    public class CardsGenerator : ISourceGenerator
    {
        private const string AnimalCardCode = @"
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace BlazorMemoryGame.Models
{
    /// <summary>
    /// <c>AnimalCard</c> displays a unique animal on each card
    /// <para>
    /// <see cref=""IEquatable{T}""/>
    /// </para>
    /// <para><see href=""https://home.unicode.org/"">type windows key + period for emoji characters on Windows</see></para>
    /// <list type=""bullet"">
    /// <itm><description><para><em>animal card</em></para></description></itm>
    /// </list>
    /// <list type=""number"">
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
                    case (false, true): return ""matched"";
                    case (true, false): return ""turned"";
                    case (true, true): return ""turned matched"";
                    default: return """";
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
}";

        private const string CardTypeCode = @"
using System;
namespace BlazorMemoryGame.Models
{{
    public class {0}Card : AnimalCard
    {{
        public override string Animal => ""{1}"";
    }}
}}";

        private const string CardHelpersCode1 = @"
using System;
using System.Collections.Immutable;

namespace BlazorMemoryGame.Models
{
    public static class CardHelpers
    {
        public static ImmutableArray<string> AllAnimalEmojis { get; } = ";

        private const string CardHelpersCode2 = @"

        public static AnimalCard Create(string emoji)
        {
            return emoji switch
            {";

        private const string CardHelpersCode3 = @"
                _ => throw new ArgumentException(nameof(emoji)),
            };
        }
    }
}";

        private const string SwitchExpression = @"
                {0} => new {1}Card(),";

        public void Execute(GeneratorExecutionContext context)
        {
            try
            {
                var emojiFile = context.AdditionalFiles.FirstOrDefault(file => string.Equals(Path.GetFileName(file.Path), "Emojis.txt", StringComparison.OrdinalIgnoreCase));
                if (emojiFile is null)
                {
                    return;
                }

                var generatorDocuments = new List<(string documentName, string source)>();
                var generatedSwitchClauses = new StringBuilder();
                var allEmojis = new StringBuilder();

                var text = emojiFile.GetText().ToString();
                using var sr = new StringReader(text);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var fields = line.Trim().Split(',');
                    var emoji = fields[0].Trim();
                    var name = fields[1].Trim();

                    var quotedEmoji = $"\"{emoji}\"";
                    generatedSwitchClauses.Append(string.Format(SwitchExpression, quotedEmoji, name));
                    allEmojis.Append($"{quotedEmoji},");
                    var generatorType = string.Format(CardTypeCode, name, emoji);
                    context.AddSource($"{name}Card", SourceText.From(generatorType, Encoding.UTF8));
                }

                // inject the created source into the users compilation
                var allEmojiList = $@"(new[]{{ {allEmojis} }}).ToImmutableArray();";
                var cardHelpers = CardHelpersCode1 + allEmojiList +
                    CardHelpersCode2 + generatedSwitchClauses.ToString() + CardHelpersCode3;

                context.AddSource("CardHelpers", SourceText.From(cardHelpers, Encoding.UTF8));
                context.AddSource("AnimalCard", SourceText.From(AnimalCardCode, Encoding.UTF8));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}
