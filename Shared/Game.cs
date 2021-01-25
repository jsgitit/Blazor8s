using System;
using System.Collections.Generic;

namespace Blazor8s.Shared
{
    public class Game
    {
        public Game()
        {
            Deck = new List<Card>();
            foreach (Suits suit in Enum.GetValues(typeof(Suits)))
            {
                foreach (CardValue value in Enum.GetValues(typeof(CardValue)))
                {
                    Deck.Add(new Card { Suit = suit, Value = value});
                }
            }
        }
        public List<Card> Deck { get; set; }
    }
}
