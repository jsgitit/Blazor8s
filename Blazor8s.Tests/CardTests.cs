using System;
using Xunit;
using Blazor8s.Shared;


namespace Blazor8s.Tests
{
    public class CardTests
    {
        [Fact]
        public void CanCreateDeck()
        {
            var game = new Game();
            Assert.Equal(52, game.Deck.Count);

        }
    }
}
