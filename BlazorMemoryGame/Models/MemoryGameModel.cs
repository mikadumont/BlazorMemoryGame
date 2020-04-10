using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace BlazorMemoryGame.Models
{
    public class MemoryGameModel
    {
        private readonly int turnDelayDuration;
        private string[] animalEmojis = new[] { "🐶", "🐺", "🐮", "🦊", "🐱", "🦁", "🐯", "🐹" };
        private Timer timer = new Timer(100);
        private DateTime? timerStart, timerEnd;
        private AnimalCard lastCardSelected;
        private bool isTurningInProgress;
        private List<double> completionTimes = new List<double>();

        public List<AnimalCard> ShuffledCards { get; set; }

        public int MatchesFound { get; private set; }

        public TimeSpan GameTimeElapsed
            => timerStart.HasValue ? timerEnd.GetValueOrDefault(DateTime.Now).Subtract(timerStart.Value) : default;

        public bool GameEnded => timerEnd.HasValue;

        public double? LatestCompletionTime => completionTimes.Count > 0
            ? completionTimes[completionTimes.Count - 1]
            : (double?)null;

        public event ElapsedEventHandler TimerElapsed
        {
            add { timer.Elapsed += value; }
            remove { timer.Elapsed -= value; }
        }

        public MemoryGameModel(int turnDelayDuration)
        {
            this.turnDelayDuration = turnDelayDuration;
            ResetGame();
        }

        public void ResetGame()
        {
            var random = new Random();
            ShuffledCards = animalEmojis.Concat(animalEmojis)
                .OrderBy(item => random.Next())
                .Select(item => new AnimalCard(item))
                .ToList();
            MatchesFound = 0;
            timerStart = timerEnd = null;
        }

        public async Task SelectCardAsync(AnimalCard card)
        {
            // Make sure the clock is running
            if (!timer.Enabled)
            {
                timerStart = DateTime.Now;
                timer.Start();
            }

            // Can't select cards that were already turned
            if (!card.IsTurned ? isTurningInProgress : true)
            {
                return;
            }

            card.IsTurned = true;

            if (lastCardSelected == null)
            {
                // First selection of the pair. Remember it.
                lastCardSelected = card;
            }
            else
            {
                if (card != lastCardSelected && card.Animal == lastCardSelected.Animal)
                {
                    // Match found!
                    MatchesFound++;
                    card.IsMatched = lastCardSelected.IsMatched = true;
                }
                else
                {
                    // Not a match
                    isTurningInProgress = true;
                    await Task.Delay(turnDelayDuration); // Pause before turning back
                    isTurningInProgress = false;
                    card.IsTurned = lastCardSelected.IsTurned = false;
                }

                // Reset for next pair.
                lastCardSelected = null;
            }

            // Is the game won?
            if (MatchesFound == animalEmojis.Length)
            {
                timerEnd = DateTime.Now;
                timer.Stop();
                completionTimes.Add(timerEnd.Value.Subtract(timerStart.Value).TotalSeconds);
            }
        }
    }
}