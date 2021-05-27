using System;
using System.Collections.Generic;
using BattleShip.BLL;

namespace BattleShipAssignment
{
    internal class GamePlay : IGamePlay
    {
        internal event EventHandler<GamePlayArgs> GamePlayCompleted;

        private IGamePlayService _gamePlayService;
        private IBattleshipBoardService _battleshipBoardService;

        public GamePlay(IGamePlayService gamePlayService, IBattleshipBoardService battleshipBoardService)
        {
            _gamePlayService = gamePlayService;
            _battleshipBoardService = battleshipBoardService;
        }
        /// <summary>
        /// Starting battleship game
        /// </summary>
        public void Start()
        {
            var battleshipBoard = _battleshipBoardService.GetBattleshipBoard();
            var players = _battleshipBoardService.GetPlayers();

            _gamePlayService.GenerateGamePlayBoard(battleshipBoard, players);
            _gamePlayService.PlayGame();
            _gamePlayService.FindWinner();

            OnGamePlayCompleted(_gamePlayService.GetGameOutput());
        }
        
        /// <summary>
        /// For invoking game completion event
        /// </summary>
        /// <param name="output"></param>
        protected virtual void OnGamePlayCompleted(IEnumerable<string> output)
        {
            GamePlayCompleted?.Invoke(this, new GamePlayArgs() { Output = output });
        }
    }
}