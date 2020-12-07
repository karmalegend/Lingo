using Lingo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lingo.Data.Interfaces
{
    public class GameRepository : IGameRepo
    {

        private readonly LingoContext _context;

        public GameRepository(LingoContext lingoContext)
        {
            _context = lingoContext;
        }


        public void addGameSession(gameSessionModel gameSession)
        {
            _context.gameSessions.Add(gameSession);
        }

        public void updateGameSession(gameSessionModel gameSession) {
            _context.gameSessions.Update(gameSession);
        }


        public gameSessionModel getCurrentGame(string username) {
            return _context.gameSessions
                .Include( g => g.player)
                .Where(g => g.player.Username.Equals(username))
                .FirstOrDefault();
        }


        public bool saveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
