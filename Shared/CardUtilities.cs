using System;
using System.Collections.Generic;
using System.Linq;

namespace Blazor8s.Shared
{
    public static class CardUtilities
    {
        static readonly Random random = new((int)DateTime.Now.Ticks); // this is the 'right way' to truly seed random

        // wiki on Fisher Yates Shuffle method...
        public static List<T> Shuffle<T>(this IList<T> source)
        {

            List<T> result = new(source);
            int count = result.Count;
            while (count > 1)
            {
                --count;
                int k = random.Next(count + 1);
                var value = result[k];
                result[k] = result[count];
                result[count] = value;
            }
            return result;
        }

        public static List<Card> GetShuffledDeck() =>
            Enum.GetValues(typeof(CardSuit))
                .Cast<CardSuit>()
                .Select(suit =>
                    (Suit: suit, Values: Enum.GetValues(typeof(CardValue)).Cast<CardValue>()))
                .SelectMany(pair => pair.Values, (pair, value) => new Card(value, pair.Suit))
                .ToList()
                .Shuffle();
    
        // the code below was refactored to the code above
        // my preference would still be to use the nested foreach for readability. Agree?

        //{
        //    List<Card> deck = new();
        //    foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
        //    {
        //        foreach (CardValue value in Enum.GetValues(typeof(CardValue)))
        //        {
        //            deck.Add(new(value, suit));
        //        }
        //    }
        //    return deck;
        //}


    }
}
