using Lingo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingo.Services.Interfaces
{
    public interface IGameService
    {
        public gameSessionModel createNewGameForUser(string Username);
    }
}
