﻿using Blazor8s.Shared;
using System;
using System.Collections.Generic;

namespace Blazor8s.Client
{
    public class ClientGameState
    {
        public Guid Id { get; set; }
        public List<Card> Hand { get; set; } = new();
        public bool HasGameStarted { get; set; } = false;
        public Card SelectedCard { get; set; }
    }
}
