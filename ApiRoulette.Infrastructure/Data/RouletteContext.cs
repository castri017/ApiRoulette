using ApiRoulette.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiRoulette.Infrastructure.Data
{
    public partial class RouletteContext : DbContext
    {
        public RouletteContext()
        {
        }

        public RouletteContext(DbContextOptions<RouletteContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bet> Bet { get; set; }
        public virtual DbSet<Roulette> Roulette { get; set; }

   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bet>(entity =>
            {
                entity.HasKey(e => e.IdBet);

                entity.Property(e => e.Color)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Roulette>(entity =>
            {
                entity.HasKey(e => e.IdRoulette);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StarDate).HasColumnType("datetime");

                entity.Property(e => e.State).HasDefaultValueSql("((0))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
