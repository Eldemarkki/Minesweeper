using System;

namespace Minesweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            MinesweeperGame game = new MinesweeperGame(10, 10, 15);
            game.Print();

            while (true)
            {
                Console.Write("> ");
                string[] commandArgs = Console.ReadLine().Split(" ");
                string command = commandArgs[0].ToLower().Trim();
                if (command == "open")
                {
                    int x = int.Parse(commandArgs[1]);
                    int y = int.Parse(commandArgs[2]);
                    Tile tile = game.GetTile(x, y);
                    if (tile.IsFlagged)
                    {
                        Console.Write("You have flagged this tile as a potential bomb. Are you sure you want to open this (y/n) ");
                        bool open = Console.ReadLine().ToLower().Trim() == "y";
                        if (open)
                        {
                            game.Open(x, y, out bool wasBombTile);
                            if (wasBombTile)
                            {
                                Console.WriteLine("Game over, you hit a bomb!");
                                game.Print();
                                break;
                            }
                            else
                            {
                                game.Print();
                            }
                        }
                    }
                    else
                    {
                        game.Open(x, y, out bool wasBombTile);
                        if (wasBombTile)
                        {
                            Console.WriteLine("Game over, you hit a bomb!");
                            game.Print();
                            break;
                        }
                        else
                        {
                            game.Print();
                        }
                    }
                }
                else if (command == "flag")
                {
                    int x = int.Parse(commandArgs[1]);
                    int y = int.Parse(commandArgs[2]);
                    game.Flag(x, y);
                    game.Print();
                }
            }
        }
    }
}
