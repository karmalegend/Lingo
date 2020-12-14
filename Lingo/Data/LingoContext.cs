using Lingo.Models;
using Microsoft.EntityFrameworkCore;

namespace Lingo.Data
{
    public class LingoContext : DbContext
    {
        public LingoContext(DbContextOptions<LingoContext> opt) : base(opt) { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<GameSessionModel> GameSessions { get; set; }
        public DbSet<FiveLetterWordModel> FiveLetterWords { get; set; }
        public DbSet<SixLetterWordModel> SixLetterWords { get; set; }
        public DbSet<SevenLetterWordModel> SevenLetterWords { get; set; }
        public DbSet<HighScoreModel> HighScores { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<FiveLetterWordModel>()
                .HasIndex(w => w.Word)
                .IsUnique();
            modelBuilder.Entity<SixLetterWordModel>()
                .HasIndex(w => w.Word)
                .IsUnique();
            modelBuilder.Entity<SevenLetterWordModel>()
                .HasIndex(w => w.Word)
                .IsUnique();
            modelBuilder.Entity<UserModel>()
                .HasIndex(u => u.Username)
                .IsUnique();
            modelBuilder.Entity<UserModel>()
                .Property(u => u.Password).IsUnicode(false);
        }
    }
}
