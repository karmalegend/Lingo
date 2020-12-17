using AutoMapper;
using Lingo.Controllers;
using Lingo.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LingoTest.ControllerTests
{
    [TestClass]
    public class GameControllerTest
    {
        private readonly GameController _gameController;
        private readonly Mock<IGameService> _gameService = new Mock<IGameService>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();

        public GameControllerTest()
        {
            _gameController = new GameController(_gameService.Object, _mapper.Object);
        }

        //https://stackoverflow.com/questions/1877225/how-do-i-unit-test-a-controller-method-that-has-the-authorize-attribute-applie
        [TestMethod]
        public void CreateNewGameSessionTest()
        {

        }
    }
}
