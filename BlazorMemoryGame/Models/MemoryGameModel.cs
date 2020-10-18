using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;

namespace BlazorMemoryGame.Models
{
    // Add debugger display attribute
    public class MemoryGameModel
    {
        private readonly int turnDelayDuration;
        private string[] animalEmojis = new[] { "🐶", "🐺", "🐮", "🦊", "🐱", "🦁", "🐯", "🐹" };
        private Timer timer = new Timer(100);
        private DateTime? timerStart, timerEnd;
        private AnimalCard lastCardSelected;
        private bool isTurningInProgress;
        private List<double> completionTimes = new List<double>();
        public bool playerTurn;
        public string winner;
        public bool developerMode;

        public List<AnimalCard> ShuffledCards { get; set; }
        
        public int MatchesFound { get; private set; }
        public int MatchesFoundP1 { get; private set; }
        public int MatchesFoundP2 { get; private set; }

        public TimeSpan GameTimeElapsed
            => timerStart.HasValue ? timerEnd.GetValueOrDefault(DateTime.Now).Subtract(timerStart.Value) : default;

        public bool GameEnded => timerEnd.HasValue;
        
        // New C# 8 Index Operator 
        public double? LatestCompletionTime => completionTimes.Count > 0 ? completionTimes[completionTimes.Count - 1] : (double?)null;

        public bool DeveloperMode { get => developerMode; set => developerMode = value; }

        public event ElapsedEventHandler TimerElapsed
        {
            add { timer.Elapsed += value; }
            remove { timer.Elapsed -= value; }
        }

        public MemoryGameModel(int turnDelayDuration)
        {
            this.turnDelayDuration = turnDelayDuration;
            playerTurn = true;
            ResetGame();
        }

        public void ResetGame()
        {
            MatchesFound = 0;
            timerStart = timerEnd = null;
            winner = "Not found";
            developerMode = false;

            var random = new Random();
            // Wrap call chain
            ShuffledCards = animalEmojis.Concat(animalEmojis).OrderBy(item => random.Next()).Select(item => AnimalCard.Create(item)).ToList();
        }
        
        public async Task SelectCardAsync(AnimalCard card)
        {
            if (!timer.Enabled)
            {
                timerStart = DateTime.Now;
                timer.Start();
            }

            // Simplify conditional expression
            if (!card.IsTurned ? isTurningInProgress : true)
            {
                return;
            }

            card.IsTurned = true;

            if (lastCardSelected == null)
            {
                lastCardSelected = card;
            }
            else
            {
                if (card == lastCardSelected)
                {
                    // Convert to switch expression
                    if (playerTurn == true) //Player 1 = true 
                    { 
                        MatchesFoundP1++; 
                    }
                    else
                    {
                        MatchesFoundP2++;
                    }

                    MatchesFound++;
                    card.IsMatched = lastCardSelected.IsMatched = true;
                }
                else
                {
                    isTurningInProgress = true;
                    await Task.Delay(turnDelayDuration); // Pause before turning back
                    isTurningInProgress = false;
                    playerTurn = !playerTurn;
                    card.IsTurned = lastCardSelected.IsTurned = false;
                }

                lastCardSelected = null;
            }

            // IntelliSense in DateTime and TimeSpan literals
            string date = DateTime.Now.ToString("mm:");
            DateTime dt = new DateTime(2020, 10, 15, 8, 30, 52);

            Regex r = new Regex("");
            
            if (MatchesFound == animalEmojis.Length || DeveloperMode == true)
            {
                timerEnd = DateTime.Now;
                timer.Stop();
                completionTimes.Add(timerEnd.Value.Subtract(timerStart.Value).TotalSeconds);
                FindWinner();
            }
        }

        public void FindWinner()
        {
            if (MatchesFoundP1 > MatchesFoundP2)
            {
                winner = "Player 1 wins!";
            }
            else if (MatchesFoundP2 > MatchesFoundP1)
            {
                winner = "Player 2 wins!";
            }
            else 
            {
                winner = "Tie!";
            }
        }

        public void EnableDeveloperMode()
        {
            this.developerMode = true;
        }
    }
}