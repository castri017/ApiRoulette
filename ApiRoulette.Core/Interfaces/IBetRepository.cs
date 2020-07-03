using ApiRoulette.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiRoulette.Core.Interfaces
{
    public interface IBetRepository
    {
        Task<Bet> CreateBet(Bet bet, string idUser);
    }
}
