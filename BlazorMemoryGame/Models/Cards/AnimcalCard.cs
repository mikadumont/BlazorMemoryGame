namespace BlazorMemoryGame.Models
{
    public class AnimalCard
    {
        public AnimalCard(string animal)
        {
            Animal = animal;
        }

        public string Animal { get; }
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
    }
}
