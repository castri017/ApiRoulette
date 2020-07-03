using ApiRoulette.Core.Entities;
using ApiRoulette.Core.Interfaces;
using ApiRoulette.Infrastructure.Data;
using Microsoft.IdentityModel.SecurityTokenService;
using System;
using System.Threading.Tasks;
namespace ApiRoulette.Infrastructure.Repositories

{
    public class BetRepository : IBetRepository
    {
        private readonly RouletteContext _context;
        public BetRepository(RouletteContext context)
        {
            _context = context;
        }

        public async Task<Bet> CreateBet(Bet bet, string idUser)
        {
            var roulette = await _context.Roulette.FindAsync(bet.IdRoulette); 
            if (roulette == null)
            {
                throw new BadRequestException("Ruleta no existe.");
            } 
            Bet betplay = new Bet()
            {
                IdRoulette = bet.IdRoulette,
                IdUser= idUser,
                Money = bet.Money,
                Position = bet.Position,
                Color = bet.Color 

            };
            validation(betplay);
            _context.Bet.Add(betplay);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new BadRequestException("Error al registrar la apuesta");
            }
            return betplay; 
        }

        public void validation(Bet bet)
        {
            if (!String.IsNullOrEmpty(bet.Color) && (-1 != bet.Position))
            {
                throw new BadRequestException("La apuesta es valida solo al color o posicion, no se puede ambos");
            }
            else if (bet.Color == "")
            {
                if (bet.Money < 1 || bet.Money > 10000)
                    throw new BadRequestException("El monto debe estar entre 0.1 y 10000 dolares para poder apostar.");
                if (bet.Position < 0 || bet.Position > 36)
                    throw new BadRequestException("El numero de la posicion debe estar entre 0 y 36");
            }
            else
            {
                if (bet.Money < 1 || bet.Money > 10000)
                    throw new BadRequestException("El rango de apuesta es entre 0.5 y 10000 dolares para poder apostar.");
                if (bet.Color.ToString() != "rojo" && bet.Color.ToString() != "negro")
                    throw new BadRequestException("Color invalido, debe ser rojo o negro.");
            }
        }
    }
}
