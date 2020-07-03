using System;
using System.Collections.Generic;

namespace ApiRoulette.Core.Entities
{
    public partial class Bet
    {
        public int IdBet { get; set; }
        public int IdRoulette { get; set; }
        public string IdUser { get; set; }
        public double Money { get; set; }
        public int? Position { get; set; }
        public string Color { get; set; }
    }
}
