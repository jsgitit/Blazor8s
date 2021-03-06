﻿using Blazor8s.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor8s.Server.Hubs
{
    public class GameState
    {
        public List<Player> Players { get; set; } = new();
        public bool HasGameStarted { get; set; }
        public Stack<Card> Deck { get; set; } = 
                new Stack<Card>(CardUtilities.CreateDeck().Shuffle());
        public Card LastDiscard { get; set; }
    }

    public class Player
    {


        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; }
        public List<Card> Hand { get; set; } = new();

    }


}
