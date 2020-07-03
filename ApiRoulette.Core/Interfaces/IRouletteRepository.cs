using ApiRoulette.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiRoulette.Core.Interfaces
{
    public interface IRouletteRepository
    {
        Task<Roulette> CreateRoulette();
        Task<string> OpenRoulette(int idRoulette);
        Task<IEnumerable<Bet>> CloseRoulette(int idRoulette);
        Task<List<Roulette>> ListRoulette(); 
    }
}
