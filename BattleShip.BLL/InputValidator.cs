using BattleShip.BLL;
using BattleShip.BLL.Constants;
using BattleShip.BLL.Extensions;
using BattleShip.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BattleShip.BLL
{
    public class InputValidator : IInputValidator
    {
        private IBattleshipBoardService _battleshipBoardService;
        private BattleshipBoard _battleshipBoard;

        public InputValidator(IBattleshipBoardService battleshipBoardService)
        {
            _battleshipBoardService = battleshipBoardService;
            _battleshipBoard = _battleshipBoardService.GetBattleshipBoard();
        }
        /// <summary>
        /// Validating battleship board input
        /// </summary>
        /// <param name="key"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public InputValidationResult Validate(string key, IList<string> input)
        {
            InputValidationResult inputValidationResult = new InputValidationResult();
            inputValidationResult.Key = key;
            inputValidationResult.Input = input;
           // bool isInputValid = false;
            switch (key)
            {
                case AppConstants.BattleAreaWidthAndHeight:
                    if (input.Count == 2)
                    {
                        if (!UInt16.TryParse(input[0], out ushort boardWidth))
                            break; //invalid width

                        if (!Regex.IsMatch(input[1], @"[a-zA-Z]"))
                            break; //invalid height

                        ushort boardHeight = (ushort)(input[1].FirstCharUpper() - 64);
                        
                        _battleshipBoard.Width = boardWidth;
                        _battleshipBoard.Height = boardHeight;
                        _battleshipBoard.HeightCharacter = input[1].FirstCharUpper();

                        inputValidationResult.IsInputValid = true;
                    }
                    break;

                case AppConstants.NumberOfBattleship:
                    if (input.Count == 1)
                    {
                        if (UInt16.TryParse(input[0], out ushort numberOfBattleship))
                        {
                           if ((numberOfBattleship <= _battleshipBoard.Width * _battleshipBoard.Height) && numberOfBattleship >= 1)
                            {
                                _battleshipBoard.CurrentInputShip = numberOfBattleship;
                                inputValidationResult.IsInputValid = true;
                            }
                        }
                    }
                    break;

                case AppConstants.BattleshpWidthHeightAndCoordinatesForPlayer1Player2:
                    if (input.Count != 5)
                        break; //invalid input

                    Battleship player1Battleship = new Battleship();

                    #region Get Battleship type from input

                    if (input[0].Length != 1)
                        break;//invalid battleship type

                    if (string.Compare(input[0], "Q", true) == 0)
                        player1Battleship.Type = BattleshipType.TypeQ;
                    else if (string.Compare(input[0], "P", true) == 0)
                        player1Battleship.Type = BattleshipType.TypeP;
                    else
                        break;//invalid battleship type

                    #endregion Get Battleship type from input

                    #region Get Battleship Width and Height

                    if (!UInt16.TryParse(input[1], out ushort battleshipWidth))
                        break; //invalid width
                    else
                    {                        
                        if (battleshipWidth <= _battleshipBoard.Width && battleshipWidth >= 1)
                            player1Battleship.Width = battleshipWidth;
                        else
                            break;//invalid battleship width
                    }

                    if (!UInt16.TryParse(input[2], out ushort battleshipHeight))
                        break; //invalid height
                    else
                    {
                        if (battleshipHeight <= _battleshipBoard.Height && battleshipHeight >= 1)
                            player1Battleship.Height = battleshipHeight;
                        else
                            break;//invalid battleship width
                       
                        player1Battleship.HeightCharacter = Convert.ToChar(battleshipHeight + 64);
                    }

                    #endregion Get Battleship Width and Height

                    #region Get Battleship Position for Player 1 and Player 2

                    #region Get X Coordinate Y Coordinate for Player 1

                    if (!IsXCoordinateValid(input[3]))
                        break; //invalid X Coordinate

                    player1Battleship.PositionX = (ushort)(input[3].FirstCharUpper() - 64);

                    if (!IsYCoordinateValid(input[3]))
                        break;//invalid Y Coordinate

                    if (UInt16.TryParse(input[3].Substring(1), out ushort shipYCoordinatePlayer1))
                        player1Battleship.PositionY = shipYCoordinatePlayer1;

                    player1Battleship.PositionString = input[3];

                    #endregion Get X Coordinate Y Coordinate for Player 1

                    //Player two battleship
                    var player2Battleship = (Battleship)player1Battleship.Clone();

                    #region Get X Coordinate Y Coordinate for Player 2

                    if (!IsXCoordinateValid(input[4]))
                        break; //invalid X Coordinate

                    player2Battleship.PositionX = (ushort)(input[4].FirstCharUpper() - 64);

                    if (!IsYCoordinateValid(input[4]))
                        break;//invalid Y Coordinate

                    if (UInt16.TryParse(input[4].Substring(1), out ushort shipYCoordinatePlayer2))
                        player2Battleship.PositionY = shipYCoordinatePlayer2;

                    player2Battleship.PositionString = input[4];

                    #endregion Get X Coordinate Y Coordinate for Player 2

                    #endregion Get Battleship Position for Player 1 and Player 2

                    _battleshipBoard.Players[0].Battleships.Add(player1Battleship);
                    _battleshipBoard.Players[1].Battleships.Add(player2Battleship);

                    inputValidationResult.IsInputValid = true;
                    break;

                case AppConstants.SequenceOfMissileTargetLocationPlayer1:
                    if (input.Count < 1)
                        break;
                    else
                    {
                        if (!IsTargetCoordinatesValid(input))
                            break;

                        _battleshipBoard.Players[0].TargetPositionString = input;
                        inputValidationResult.IsInputValid = true;
                    }
                    break;

                case AppConstants.SequenceOfMissileTargetLocationPlayer2:
                    if (input.Count < 1)
                        break;
                    else
                    {
                        if (!IsTargetCoordinatesValid(input))
                            break;

                        _battleshipBoard.Players[1].TargetPositionString = input;
                        inputValidationResult.IsInputValid = true;
                    }
                    break;

                default:
                    inputValidationResult.IsInputValid = false;
                    break;
            };
            return inputValidationResult;
        }

        /// <summary>
        /// Validating both X and Y target coordinates
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool IsTargetCoordinatesValid(IList<string> input)
        {
            foreach (var targetCoordinate in input)
            {
                if (!IsXCoordinateValid(targetCoordinate))
                    return false;
                if (!IsYCoordinateValid(targetCoordinate))
                    return false;
            }

            return true;
        }

        private bool IsYCoordinateValid(string coordinate)
        {
            if (!UInt16.TryParse(coordinate.Substring(1), out ushort shipYCoordinatePlayer1))
                return false; //invalid Y Coordinate
            else
            {
                if (shipYCoordinatePlayer1 <= _battleshipBoard.Height && shipYCoordinatePlayer1 >= 1)
                    return true;
                else
                    return false;//invalid Y Coordinate
            }
        }

        private bool IsXCoordinateValid(string coordinate)
        {
            if (Regex.IsMatch(coordinate.Substring(0), @"[a-zA-Z]"))
            {
                ushort shipXCoordinatePlayer1 = (ushort)(coordinate.FirstCharUpper() - 64);
               
                if (shipXCoordinatePlayer1 <= _battleshipBoard.Width && shipXCoordinatePlayer1 >= 1)
                {
                    //valid
                    return true;
                }
                else
                {
                    //invalid X Coordinate
                    return false;
                }
            }
            else
                return false;
        }             
    }
}