using System.Linq;
using Lingo.Data.Interfaces;
using Lingo.Models;
using Microsoft.EntityFrameworkCore;

namespace Lingo.Data.Repository
{
    public class GameRepository : IGameRepo
    {

        private readonly LingoContext _context;

        public GameRepository(LingoContext lingoContext)
        {
            _context = lingoContext;
        }


        public void AddGameSession(GameSessionModel gameSession)
        {
            _context.GameSessions.Add(gameSession);
        }

        public void UpdateGameSession(GameSessionModel gameSession) {
            _context.GameSessions.Update(gameSession);
        }


        public GameSessionModel GetCurrentGame(string username) {
            return _context.GameSessions
                .Include(g => g.Player)
                .FirstOrDefault(g => g.Player.Username.Equals(username));
        }


        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
