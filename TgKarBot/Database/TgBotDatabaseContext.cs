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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Constants.Database.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ask>().ToTable("Asks");
        }
    }
}
