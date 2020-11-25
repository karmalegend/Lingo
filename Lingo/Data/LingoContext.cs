using Lingo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingo.Data
{
    public class LingoContext : DbContext
    {
        public LingoContext(DbContextOptions<LingoContext> opt) : base(opt) { }

        public DbSet<userModel> users { get; set; }
        public DbSet<gameSessionModel> gameSessions { get; set; }
        public DbSet<fiveLetterWordModel> fiveLetterWords { get; set; }
        public DbSet<sixLetterWordModel> sixLetterWords { get; set; }
        public DbSet<sevenLetterWordModel> sevenLetterWords { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<fiveLetterWordModel>()
                .HasIndex(w => w.word)
                .IsUnique();
            modelBuilder.Entity<sixLetterWordModel>()
                .HasIndex(w => w.word)
                .IsUnique();
            modelBuilder.Entity<sevenLetterWordModel>()
                .HasIndex(w => w.word)
                .IsUnique();
            modelBuilder.Entity<userModel>()
                .HasIndex(u => u.Username)
                .IsUnique();
            modelBuilder.Entity<userModel>()
                .Property(u => u.Password).IsUnicode(false);
        }
    }
}
