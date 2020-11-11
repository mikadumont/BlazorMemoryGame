using System;
using System.IO;

namespace BlazorMemoryGame.Models
{
    public class FoxCard : AnimalCard
    {
        public override string Animal => "🦊";


        public static void Fox()
        {
            try { }
            catch (Exception e)
            {
                LogError(e);
            }
        }
        private static void LogError(Exception e)
        {
            Console.WriteLine("What does the fox say?");
        }
    }
}