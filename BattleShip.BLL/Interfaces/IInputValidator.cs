using BattleShip.BLL.Models;
using System.Collections.Generic;

namespace BattleShip.BLL
{
    public interface IInputValidator
    {
        InputValidationResult Validate(string key, IList<string> input);
        bool IsTargetCoordinatesValid(IList<string> input);
    }
}