using BattleShip.BLL;
using BattleShip.BLL.Constants;
using BattleShip.BLL.Services;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BattleShipTest
{
    [TestFixture]
    public class BattleShipTest
    {
        private IBattleshipBoardService _battleshipBoardService;
        private IGamePlayService _gamePlayBoardService;
        private InputValidator _inputValidator;
        [SetUp]
        public void SetupBattleShipBoard()
        {

            _battleshipBoardService = new BattleshipBoardService();
        }
        [SetUp]
        public void SetupGamePlayBoard()
        {

            _gamePlayBoardService = new GamePlayService();
        }
        [SetUp]
        public void SetupInputVaidator()
        {
          
            _inputValidator = new InputValidator(_battleshipBoardService);
        }


        [Test]
        public void WinGame()
        {
            _inputValidator.Validate(AppConstants.BattleAreaWidthAndHeight, new List<string>() { "5", "E" });
            _inputValidator.Validate(AppConstants.NumberOfBattleship, new List<string>() { "2" });
            _inputValidator.Validate(AppConstants.BattleshpWidthHeightAndCoordinatesForPlayer1Player2, new List<string>() { "Q", "1", "1", "A1", "B2" });
            _inputValidator.Validate(AppConstants.BattleshpWidthHeightAndCoordinatesForPlayer1Player2, new List<string>() { "P", "2", "1", "D4", "C3" });
            _inputValidator.Validate(AppConstants.SequenceOfMissileTargetLocationPlayer1, new List<string>() { "A1", "B2", "B2", "B3" });
            _inputValidator.Validate(AppConstants.SequenceOfMissileTargetLocationPlayer2, new List<string>() { "A1", "B2", "B3", "A1", "D1", "E1", "D4", "D4", "D5", "D5" });

            var battleshipBoard = _battleshipBoardService.GetBattleshipBoard();
            var players = _battleshipBoardService.GetPlayers();

            _gamePlayBoardService.GenerateGamePlayBoard(battleshipBoard, players);
            _gamePlayBoardService.PlayGame();
            _gamePlayBoardService.FindWinner();
            var result = _gamePlayBoardService.GetGameOutput();
            var expectedResult = new List<string>()
            {
                "Player 1 fires a missile with target A1 which got miss",
                "Player 2 fires a missile with target A1 which got hit",
                "Player 2 fires a missile with target B2 which got miss",
                "Player 1 fires a missile with target B2 which got hit",
                "Player 1 fires a missile with target B2 which got hit",
                "Player 1 fires a missile with target B3 which got miss",
                "Player 2 fires a missile with target B3 which got miss",
                "Player 1 has no more missile left to launch",
                "Player 2 fires a missile with target A1 which got hit",
                "Player 2 fires a missile with target D1 which got miss",
                "Player 1 has no more missile left to launch",
                "Player 2 fires a missile with target E1 which got miss",
                "Player 1 has no more missile left to launch",
                "Player 2 fires a missile with target D4 which got hit",
                "Player 2 fires a missile with target D4 which got miss",
                "Player 1 has no more missile left to launch",
                "Player 2 fires a missile with target D5 which got hit",
                "Player 2 fires a missile with target D5 which got miss",
                "Player 2 won the battle"
            };

            CollectionAssert.AreEqual(expectedResult, result.ToList());
        }

        [Test]
        public void DrawGame()
        {
            
            _inputValidator.Validate(AppConstants.BattleAreaWidthAndHeight, new List<string>() { "5", "E" });
            _inputValidator.Validate(AppConstants.NumberOfBattleship, new List<string>() { "2" });
            _inputValidator.Validate(AppConstants.BattleshpWidthHeightAndCoordinatesForPlayer1Player2, new List<string>() { "Q", "1", "1", "A1", "B2" });
            _inputValidator.Validate(AppConstants.BattleshpWidthHeightAndCoordinatesForPlayer1Player2, new List<string>() { "P", "2", "1", "D4", "C3" });
            _inputValidator.Validate(AppConstants.SequenceOfMissileTargetLocationPlayer1, new List<string>() { "A1", "B3", "B2", "B4" });
            _inputValidator.Validate(AppConstants.SequenceOfMissileTargetLocationPlayer2, new List<string>() { "A1", "B4", "B5", "B3", "B3", "E1","D5", "D5", "D4", "D4" });

            var battleshipBoard = _battleshipBoardService.GetBattleshipBoard();
            var players = _battleshipBoardService.GetPlayers();

            _gamePlayBoardService.GenerateGamePlayBoard(battleshipBoard, players);
            _gamePlayBoardService.PlayGame();
            _gamePlayBoardService.FindWinner();
            var result = _gamePlayBoardService.GetGameOutput();
            var expectedResult = new List<string>()
            {
                "Player 1 fires a missile with target A1 which got miss",
                "Player 2 fires a missile with target A1 which got hit",
                "Player 2 fires a missile with target B4 which got miss",
                "Player 1 fires a missile with target B3 which got miss",
                "Player 2 fires a missile with target B5 which got miss",
                "Player 1 fires a missile with target B2 which got hit",
                "Player 1 fires a missile with target B4 which got miss",
                "Player 2 fires a missile with target B3 which got miss",
                "Player 1 has no more missile left to launch",
                "Player 2 fires a missile with target B3 which got miss",
                "Player 1 has no more missile left to launch",
                "Player 2 fires a missile with target E1 which got miss",
                "Player 1 has no more missile left to launch",
                "Player 2 fires a missile with target D5 which got hit",
                "Player 2 fires a missile with target D5 which got miss",
                "Player 1 has no more missile left to launch",
                "Player 2 fires a missile with target D4 which got hit",
                "Player 2 fires a missile with target D4 which got miss",
                "Battle was draw!"
            };

            CollectionAssert.AreEqual(expectedResult, result.ToList());
        }
    }
}
