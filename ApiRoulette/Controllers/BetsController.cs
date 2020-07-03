using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiRoulette.Infrastructure.Repositories;
using ApiRoulette.Core.Interfaces;
using ApiRoulette.Core.Entities;
using ApiRoulette.Models;

namespace ApiRoulette.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BetsController : ControllerBase
    {
        private IBetRepository _betrepository;
        public BetsController(IBetRepository betrepository)
        {
            _betrepository = betrepository;
        }

        [HttpPost("CreateBet")]
        public async Task<ActionResult<Bet>> CreateBet([FromHeader(Name = "IdUser")] string UserId, [FromBody] BetEntity betEntity)
        {
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    Bet bet = new Bet
                    {
                        IdRoulette = betEntity.IdRoulette,
                        Money = betEntity.Money,
                        Position = betEntity.Position,
                        Color = betEntity.Color
                    };
                    await _betrepository.CreateBet(bet, UserId);
                    return Ok(bet);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { error = ex.Message, code = "Error al crear la apuesta" });
                }
            }
        }
    }
}