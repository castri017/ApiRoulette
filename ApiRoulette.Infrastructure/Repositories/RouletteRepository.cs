using ApiRoulette.Core.Entities;
using ApiRoulette.Core.Interfaces;
using ApiRoulette.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.IdentityModel.SecurityTokenService;

namespace ApiRoulette.Infrastructure.Repositories
{
    public class RouletteRepository : IRouletteRepository
    {
        private readonly RouletteContext _context;

        public RouletteRepository(RouletteContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Bet>> CloseRoulette(int idRoulette)
        {
            Roulette roulette = await _context.Roulette.FindAsync(idRoulette);
            var bet = _context.Bet.Where(b => b.IdRoulette == idRoulette);
            if (roulette == null)
            {
                throw new BadRequestException("Ruleta no existe.");
            }
            else if (roulette.State == true)
            {
                roulette.State = false;
                roulette.EndDate = DateTime.UtcNow;
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Error al cerrar la apuesta");
            }
            return bet.Select(b => new Bet
            {
                IdBet = b.IdBet,
                IdRoulette =b.IdRoulette,
                IdUser=b.IdUser,
                Money = b.Money,
                Position = b.Position,
                Color = b.Color
            });
        }
 
        public async Task<Roulette> CreateRoulette()
        {
            Roulette roulete = new Roulette()
            {
                State = false,
                StarDate = null,
                EndDate = null
            };
            _context.Roulette.Add(roulete);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return roulete;
        }

        public async Task<List<Roulette>> ListRoulette()
        {
            return await _context.Roulette.ToListAsync();
        } 

        public async Task<string> OpenRoulette(int idRoulette)
        {
            Roulette roulette = await _context.Roulette.FindAsync(idRoulette);
            if (roulette == null)
            {
                return "Ruleta no encontrada";
            }
            else if (roulette.State == false)
            {
                roulette.State = true;
                roulette.StarDate = DateTime.UtcNow;
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return "Denegado";
            }
            return "Exitoso";
        } 
    }
}
