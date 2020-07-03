using ApiRoulette.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using ApiRoulette.Core.Interfaces;
using ApiRoulette.Infrastructure.Data;
using System.Linq;

namespace ApiRoulette.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoulettesController : ControllerBase
    {
        private IRouletteRepository _rouletterepository;
        private readonly RouletteContext _context;
        public RoulettesController(IRouletteRepository rouletterepository, RouletteContext context)
        {
            _rouletterepository = rouletterepository;
            _context = context;
        }

        [HttpPost("CreateRoulette")]
        public async Task<ActionResult<Roulette>> CreateRoulette()
        {
            Roulette roulette = await _rouletterepository.CreateRoulette();
            return Ok(new { id = roulette.IdRoulette });
        }

        [HttpGet("OpenRoulette/{idRoulette}")]
        public async Task<ActionResult<Roulette>> OpenRoulette(int idRoulette)
        {
            try
            {
                return Ok(await _rouletterepository.OpenRoulette(idRoulette));
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message, code = "Error al abrir ruleta" });
            }
        }

        [HttpGet("CloseRoulette/{idRoulette}")]
        public async Task<ActionResult<Bet>> CloseRoulette(int idRoulette)
        {  
            try
            {
                return Ok(await _rouletterepository.CloseRoulette(idRoulette));
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message, code = "Error al cerrar ruleta" });
            }
        }
  
        [HttpGet("List")]
        public async Task<ActionResult<IEnumerable<Roulette>>> ListRoulette()
        {
            try
            {
                return Ok(await _rouletterepository.ListRoulette());
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}