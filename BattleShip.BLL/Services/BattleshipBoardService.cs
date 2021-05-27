using BattleShip.BLL.Constants;
using BattleShip.BLL.Models;
using System.Collections.Generic;

namespace BattleShip.BLL.Services
{
    public class BattleshipBoardService : IBattleshipBoardService
    {
        private BattleshipBoard _battleshipBoard;

        public BattleshipBoardService(BattleshipBoard battleshipBoard)
        {
            _battleshipBoard = battleshipBoard;
        }

        public BattleshipBoardService()
        {
            _battleshipBoard = new BattleshipBoard();
        }
        /// <summary>
        /// Return battleship game players
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Player> GetPlayers()
        {
            return _battleshipBoard.Players;
        }
        /// <summary>
        /// Returning battleship current input
        /// </summary>
        /// <returns></returns>
        public string GetCurrentInput()
        {
            return _battleshipBoard.CurrentInput;
        }
        /// <summary>
        /// Return battleship board
        /// </summary>
        /// <returns></returns>
        public BattleshipBoard GetBattleshipBoard()
        {
            if (_battleshipBoard.Players == null)
            {
                _battleshipBoard.Players = GeneratePlayers();
            }

            if (string.IsNullOrEmpty(_battleshipBoard.CurrentInput))
                _battleshipBoard.CurrentInput = AppConstants.BattleAreaWidthAndHeight;

            return _battleshipBoard;
        }
        /// <summary>
        /// Generating players for battleship game
        /// </summary>
        /// <returns></returns>
        private IList<Player> GeneratePlayers()
        {
            return new List<Player>() { new Player() { }, new Player() { } };
        }
    }
}