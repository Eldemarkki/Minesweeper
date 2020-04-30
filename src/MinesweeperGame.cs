using System;
using System.Collections.Generic;

namespace Minesweeper
{
    class MinesweeperGame
    {
        private Tile[,] board;
        private List<Point2D> bombCoordinates;

        public bool GameOver { get; private set; }
        public bool Won { get; private set; }

        public MinesweeperGame(int width, int height, int bombCount)
        {
            bombCoordinates = CreateBombCoordinates(width, height, bombCount);
            CreateBoard(width, height);
        }

        private List<Point2D> CreateBombCoordinates(int width, int height, int bombCount)
        {
            Random random = new Random();
            List<Point2D> bombLocations = new List<Point2D>(bombCount);
            for (int i = 0; i < bombCount; i++)
            {
                Point2D point = new Point2D(random.Next(0, width), random.Next(0, height));
                while (bombLocations.Contains(point))
                {
                    point = new Point2D(random.Next(0, width), random.Next(0, height));
                }

                bombLocations.Add(point);
            }

            return bombLocations;
        }

        private void CreateBoard(int width, int height)
        {
            // Create empty board with empty tiles
            board = new Tile[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Point2D coordinate = new Point2D(x, y);
                    Tile tile = new NumberTile(coordinate, 0);
                    board[x, y] = tile;
                }
            }

            // Set the bomb tiles
            for (int i = 0; i < bombCoordinates.Count; i++)
            {
                Point2D bombLocation = bombCoordinates[i];
                board[bombLocation.x, bombLocation.y] = new BombTile(bombLocation);
            }

            // Increase the bomb count around the bombs
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (board[x, y] is NumberTile tile)
                    {
                        tile.BombCount = BombCountAroundPoint(x, y);
                    }
                }
            }
        }

        public int BombCountAroundPoint(int x, int y)
        {
            int bombCount = 0;

            for (int xOffset = -1; xOffset <= 1; xOffset++)
            {
                for (int yOffset = -1; yOffset <= 1; yOffset++)
                {
                    if (xOffset == 0 && yOffset == 0) continue;

                    int newX = x + xOffset;
                    int newY = y + yOffset;

                    if (newX >= 0 && newX < board.GetLength(0) && newY >= 0 && newY < board.GetLength(1))
                    {
                        if (board[newX, newY] is BombTile)
                        {
                            bombCount++;
                        }
                    }
                }
            }

            return bombCount;
        }

        public void Open(int x, int y, out bool wasBombTile)
        {
            if (board[x, y].IsHidden == false)
            {
                wasBombTile = false;
                return;
            }

            board[x, y].IsHidden = false;
            if (board[x, y] is BombTile)
            {
                GameOver = true;
                Won = false;
                wasBombTile = true;
            }
            else
            {
                if (((NumberTile)board[x, y]).BombCount == 0)
                {
                    List<Tile> floodFillTiles = FloodFill.FloodFillArea(x, y, board, t => (t is NumberTile nt) && nt.BombCount == 0);
                    foreach (Tile tile in floodFillTiles)
                    {
                        tile.IsHidden = false;
                    }
                }

                wasBombTile = false;
            }
        }

        public void Flag(int x, int y)
        {
            board[x, y].IsFlagged = !board[x, y].IsFlagged;
        }

        public Tile GetTile(int x, int y)
        {
            return board[x, y];
        }

        public void Print()
        {
            for (int y = board.GetLength(1) - 1; y >= 0; y--)
            {
                for (int x = 0; x < board.GetLength(0); x++)
                {
                    Tile tile = board[x, y];
                    if (!tile.IsHidden)
                    {
                        Console.Write(tile);
                    }
                    else if (tile.IsFlagged)
                    {
                        Console.Write("F");
                    }
                    else if (tile.IsHidden)
                    {
                        Console.Write("O");
                    }
                }
                Console.WriteLine("");
            }
        }
    }
}
