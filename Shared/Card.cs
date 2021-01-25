using System.Linq;
using System.Threading.Tasks;

namespace Blazor8s.Shared
{
    public class Card
    {
        public Suits Suit { get; set; }
        public CardValue Value { get; set; }
        public bool IsFaceDown { get; set; } = true;
        public override string ToString() => $"{Value} of {Suit}";
        
    }
}
