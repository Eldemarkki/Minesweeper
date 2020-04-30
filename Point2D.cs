using System;
using System.Diagnostics.CodeAnalysis;

namespace Minesweeper
{
    public struct Point2D : IEquatable<Point2D>
    {
        public int x;
        public int y;

        public Point2D(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            return obj is Point2D d && Equals(d);
        }

        public bool Equals(Point2D other)
        {
            return x == other.x &&
                   y == other.y;
        }
    }
}
