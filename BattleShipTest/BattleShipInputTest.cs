using BattleShip.BLL;
using BattleShip.BLL.Constants;
using BattleShip.BLL.Models;
using BattleShip.BLL.Services;
using NUnit.Framework;
using System.Collections.Generic;

namespace BattleShipTest
{
    [TestFixture]
    public class BattleShipInputTest
    {
        private IBattleshipBoardService _battleshipBoardService;
        private IInputValidator _inputValidator;
        private BattleshipBoard _battleshipBoard;
        [SetUp]
        public void SetupBattleShip()
        {

            _battleshipBoardService = new BattleshipBoardService();
            _battleshipBoard = _battleshipBoardService.GetBattleshipBoard();
        }
       
        [SetUp]
        public void SetupInputVaidator()
        {

            _inputValidator = new InputValidator(_battleshipBoardService);
        }
        [Test]
        public void Validiate_BattleAreaWidthAndHeight_Should_Return_Success()
        {
            var validationResult= _inputValidator.Validate(AppConstants.BattleAreaWidthAndHeight, new List<string>() { "9", "E" });

            Assert.True(validationResult.IsInputValid);
        }
        [Test]
        public void Validiate_BattleAreaWidthAndHeight_Should_Return_Error()
        {
            var validationResult = _inputValidator.Validate(AppConstants.BattleAreaWidthAndHeight, new List<string>() { "12", "X" });

            Assert.True(validationResult.IsInputValid);
        }
        [Test]
        public void Validiate_NumberOfBattleship_Should_Return_Success()
        {
            _battleshipBoard.Width = 5;
            _battleshipBoard.Height = (ushort)('E' - 64);
            var validationResult = _inputValidator.Validate(AppConstants.NumberOfBattleship, new List<string>() { "3" });

            Assert.True(validationResult.IsInputValid);
        }
        [Test]
        public void Validiate_NumberOfBattleship_Should_Return_Error()
        {
            _battleshipBoard.Width = 5;
            _battleshipBoard.Height= (ushort)('E' - 64);
            var validationResult = _inputValidator.Validate(AppConstants.NumberOfBattleship, new List<string>() { "26" });

            Assert.True(validationResult.IsInputValid);
        }
        [Test]
        public void Validiate_TargetCoordinates_Should_Return_Success()
        {
            _battleshipBoard.Width = 5;
            _battleshipBoard.Height = (ushort)('E' - 64);
            var validationResult = _inputValidator.Validate(AppConstants.SequenceOfMissileTargetLocationPlayer1, new List<string>() { "A1", "B2", "B2", "B3" });

            Assert.True(validationResult.IsInputValid);
        }
        [Test]
        public void Validiate_TargetCoordinates_Should_Return_Error()
        {

            var validationResult = _inputValidator.Validate(AppConstants.SequenceOfMissileTargetLocationPlayer1, new List<string>() { "A1", "B2", "S2", "E3" });

            Assert.True(validationResult.IsInputValid);
        }
    }
}
