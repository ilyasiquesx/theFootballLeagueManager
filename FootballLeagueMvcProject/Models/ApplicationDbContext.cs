using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueMvcProject.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Fixture> Fixtures { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Championship> Championships { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Card> Cards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ChampionshipTeam>()
                .HasKey(t => new { t.ChampionshipId, t.TeamId });

            modelBuilder.Entity<ChampionshipTeam>()
                .HasOne(ct => ct.Championship)
                .WithMany(c => c.ChampionshipTeams)
                .HasForeignKey(ct => ct.ChampionshipId);

            modelBuilder.Entity<ChampionshipTeam>()
                .HasOne(ct => ct.Team)
                .WithMany(t => t.ChampionshipTeams)
                .HasForeignKey(ct => ct.TeamId);

            modelBuilder.Entity<Goal>()
                .HasOne(g => g.Fixture)
                .WithMany(f => f.Goals).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Card>()
                 .HasOne(c => c.Fixture)
                 .WithMany(f => f.Cards).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ChampionshipTeam>()
                .HasOne(ct => ct.Championship)
                .WithMany(c => c.ChampionshipTeams).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Fixture>()
                .HasOne(f => f.Championship)
                .WithMany(c => c.Fixtures).OnDelete(DeleteBehavior.Cascade);
        }

    }
}
