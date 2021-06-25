using BattleCards.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace BattleCards.Data
{
    public class BattleCardsDbContext : DbContext
    {
        public DbSet<User> Users { get; init; }

        public DbSet<Card> Cards { get; init; }

        public DbSet<UserCard> UserCards { get; init; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<UserCard>()
                .HasKey(k => new
                {
                    k.CardId,
                    k.UserId
                });
        }
    }
}
