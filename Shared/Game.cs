using System;
using System.Collections.Generic;

namespace Blazor8s.Shared
{
    public class Game
    {
        public Game()
        {
            Deck = new List<Card>();
            foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
            {
                foreach (CardValue value in Enum.GetValues(typeof(CardValue)))
                {
                    Deck.Add(new ( value, suit));
                }
            }
        }
        public List<Card> Deck { get; set; }
    }
}
