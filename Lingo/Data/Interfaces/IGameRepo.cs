using Lingo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingo.Data.Interfaces
{
    public interface IGameRepo
    {
        public void addGameSession(gameSessionModel gameSession);
        public bool saveChanges();
        public gameSessionModel getCurrentGame(string username);
        public void updateGameSession(gameSessionModel gameSession);
    }
}
