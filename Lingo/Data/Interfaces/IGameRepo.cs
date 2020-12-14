using Lingo.Models;

namespace Lingo.Data.Interfaces
{
    public interface IGameRepo
    {
        public void AddGameSession(GameSessionModel gameSession);
        public bool SaveChanges();
        public GameSessionModel GetCurrentGame(string username);
        public void UpdateGameSession(GameSessionModel gameSession);
    }
}
