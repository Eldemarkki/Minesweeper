using System;

namespace Minesweeper
{
    class NumberTile : Tile
    {
        public int BombCount { get; set; }

        public NumberTile(Point2D coordinate, int bombCount) : base(coordinate)
        {
            BombCount = bombCount;
        }

        public override string ToString()
        {
            return BombCount == 0 ? " " : Math.Clamp(BombCount, 0, 9).ToString();
        }
    }
}
