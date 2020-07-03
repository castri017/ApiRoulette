using System;
using System.Collections.Generic;

namespace ApiRoulette.Core.Entities
{
    public partial class Roulette
    {
        public int IdRoulette { get; set; }
        public bool? State { get; set; }
        public DateTime? StarDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
