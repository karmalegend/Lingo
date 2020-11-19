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
    }
}
