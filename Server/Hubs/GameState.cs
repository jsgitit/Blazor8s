using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor8s.Server.Hubs
{
    public class GameState
    {
        public List<string> Players { get; set; } = new();

    }
}
