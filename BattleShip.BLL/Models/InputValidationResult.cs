using System.Collections.Generic;

namespace BattleShip.BLL.Models
{
    public class InputValidationResult
    {
        public string Key { get; set; }
        public IList<string> Input { get; set; }
        public bool IsInputValid { get; set; }
    }
}
