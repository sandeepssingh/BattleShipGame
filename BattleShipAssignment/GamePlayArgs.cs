using BattleShip.BLL;
using System;
using System.Collections.Generic;

namespace BattleShipAssignment
{
    internal class GamePlayArgs : EventArgs
    {
        public IEnumerable<string> Output { get; set; }
        public int count { get; set; }
    }
}