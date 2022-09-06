using Microsoft.EntityFrameworkCore;
using TgKarBot.Database.Models;

namespace TgKarBot.Database
{
    internal class TgBotDatabaseContext :DbContext
    {
        public DbSet<AskModel> Asks { get; set; }
        public DbSet<AdminModel> Admins { get; set; }
        public DbSet<RewardModel> Rewards { get; set; }
        public DbSet<TeamProgressModel> TeamsProgress { get; set; }
        public DbSet<TeamModel> Teams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Constants.Database.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AskModel>().ToTable("Asks");
            modelBuilder.Entity<AdminModel>().ToTable("Admins");
            modelBuilder.Entity<RewardModel>().ToTable("Rewards");
            modelBuilder.Entity<TeamProgressModel>().ToTable("TeamProgress").HasKey(tp => new {tp.TeamId, tp.AskId});
            modelBuilder.Entity<TeamModel>().ToTable("Teams");
        }
    }
}
