using BattleShip.BLL.Models;
using System.Collections.Generic;

namespace BattleShip.BLL
{
    public interface IBattleshipBoardService
    {
        IEnumerable<Player> GetPlayers();

        BattleshipBoard GetBattleshipBoard();
    }
}