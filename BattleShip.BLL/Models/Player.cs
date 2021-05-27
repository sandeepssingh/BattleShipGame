using System.Collections.Generic;

namespace BattleShip.BLL.Models
{
    public class Player
    {
        public IList<Battleship> Battleships { get; set; } = new List<Battleship>();
        public IList<string> TargetPositionString { get; set; } = new List<string>();
    }
}