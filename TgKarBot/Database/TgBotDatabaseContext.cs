using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TgKarBot.Database.Models;

namespace TgKarBot.Database
{
    internal class TgBotDatabaseContext :DbContext
    {
        public DbSet<Ask> Asks { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<TeamProgress> TeamsProgress { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Constants.Database.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ask>().ToTable("Asks");
            modelBuilder.Entity<Admin>().ToTable("Admins");
            modelBuilder.Entity<Reward>().ToTable("Asks");
            modelBuilder.Entity<TeamProgress>().ToTable("Admins");
            modelBuilder.Entity<Team>().ToTable("Admins");
        }
    }
}
