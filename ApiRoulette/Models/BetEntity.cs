using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRoulette.Models
{
    public class BetEntity
    {
        public int IdRoulette { get; set; }
        public double Money { get; set; }
        public int? Position { get; set; }
        public string Color { get; set; }
    }
}
