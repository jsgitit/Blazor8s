using System.Linq;
using System.Threading.Tasks;

namespace Blazor8s.Shared
{
    // This is the old Card class...
    
    //public class Card
    //{
    //    public Card(CardValue value, CardSuit suit)
    //    {
    //        Value = value;
    //        Suit = suit;
    //    }
    //    public CardSuit Suit { get; set; }
    //    public CardValue Value { get; set; }
    //    public bool IsFaceDown { get; set; } = true;
    //    public override string ToString() => $"{Value} of {Suit}"; 
    //}
 
    
    // We refactored Card to a record type (value type)
    public record Card
    (
        CardValue Value,  // remember that the order of fields are important, for existing method signatures.
        CardSuit Suit,
        bool IsFaceDown = true
    )
    {
        public override string ToString() => $"{Value} of {Suit}";
    }
}
