using BattleShip.BLL.Models;
using System.Collections.Generic;

namespace BattleShip.BLL
{
    public interface IGamePlayService
    {
        void FillBoardWithShips(int[,] board, IList<Battleship> battleships);
        void GenerateGamePlayBoard(BattleshipBoard battleshipBoard, IEnumerable<Player> players);
        void PlayGame();
        void FindWinner();
        IEnumerable<string> GetGameOutput();
    }
}