using BattleShip.BLL.Services;
using BattleShipAssignment.InteractiveConsole;
using System;

namespace BattleShipAssignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var exit = false;
            Console.WriteLine("Welcome to Battleship game!");
            Console.WriteLine("Make your choice:");
            Console.WriteLine("Enter 1 to read from file");
            Console.WriteLine("Enter 2 to read input from console");
            string choice = Console.ReadLine();
            if (choice != "1" && choice != "2")
            {

                exit = true;
                Console.WriteLine("\nYou have made a wrong choice");
                Console.WriteLine("Press any key to exit the game");
                Console.ReadKey();
            }
            else if (choice == "2")
            {                
                Console.WriteLine("There are two type of ships - type P and type Q. Type P ships can be destroyed by a single hit in each of their cells and type Q ships require 2 hits in each of their cells.");
                Console.WriteLine("A ship is considered destroyed when all of its cells are destroyed. The player who destroys all the ships of other player first wins the game.");
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("Rules:");
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("1 <= Width of Battle area(M') <= 9");
                Console.WriteLine("A <= Height of Battle area(N') <= Z");
                Console.WriteLine("1 <= Number of battleships <= M'*N'");
                Console.WriteLine("Type of ship = {'P','Q'}");
                Console.WriteLine("1 <= Width of battleship <= M'");
                Console.WriteLine("A <= Height of battleship <= N'");
                Console.WriteLine("1 <= X coordinate of ship <= M'");
                Console.WriteLine("A <= Y coordinate of ship <= N'");
                Console.WriteLine("Note: Use space ' ' to seperate your input.");
                Console.WriteLine("Type 'EXIT' to quit the game.");
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine();

               
            }
            else
            {
                FileUtil.IsReadFromFile = true;
            }


            if (!exit)
            {
                Console.Write("Enter width and height of battle area (for example: 5 E): ");
                var battleshipBoardService = new BattleshipBoardService();
                var gamePlayService = new GamePlayService();

                var interpreter = new Interpreter(battleshipBoardService, gamePlayService);

                int lineNumber = 1;
                while (!exit)
                {
                    ICommand command;

                    // Read From File
                    //Input will be read from Input.txt present in Bin folder and output will be written to Output.txt present in bin folder of BattleShipAssignment
                    if (FileUtil.IsReadFromFile)
                    {
                        var inputText = FileUtil.Instance.ReadFromFile(battleshipBoardService.GetCurrentInput(), lineNumber++);
                        Console.Write(inputText + "\n");
                        command = interpreter.Parse(battleshipBoardService.GetCurrentInput() + inputText);
                       
                    }
                    else
                    {
                        // Read From Console
                        command = interpreter.Parse(battleshipBoardService.GetCurrentInput() + Console.ReadLine());

                    }
                    exit = command.Execute();
                }
            }
        }
    }
}