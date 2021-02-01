using System;
using System.Collections.Generic;

namespace Blazor8s.Shared
{
    //public class Player
    //{

    //    public Guid Id { get; } = Guid.NewGuid();
    //    public string Name { get; set; }
    //    public List<Card> Hand { get; set; } = new();
    //}

    // Converted Player from a class to a record.
    public record Player
    {
        public Guid Id { get; } = Guid.NewGuid();
        public HashSet<Card> Hand { get; init; } = new();
        public string Name { get; init; }
    }
}

