using System;
using System.Collections.Generic;

namespace Minesweeper
{
    public static class FloodFill
    {
        public static List<T> FloodFillArea<T>(int x, int y, T[,] area, Predicate<T> filter)
        {
            return FloodFillArea(x, y, area, filter, new List<T>());
        }

        private static List<T> FloodFillArea<T>(int x, int y, T[,] area, Predicate<T> filter, List<T> neighbors)
        {
            if (IsInsideArea(x, y, area) && filter(area[x, y]) && !neighbors.Contains(area[x, y]))
            {
                neighbors.Add(area[x, y]);
            }
            if (IsInsideArea(x + 1, y, area) && filter(area[x + 1, y]) && !neighbors.Contains(area[x + 1, y]))
            {
                neighbors.AddRange(FloodFillArea(x + 1, y, area, filter, neighbors));
            }
            if (IsInsideArea(x - 1, y, area) && filter(area[x - 1, y]) && !neighbors.Contains(area[x - 1, y]))
            {
                neighbors.AddRange(FloodFillArea(x - 1, y, area, filter, neighbors));
            }
            if (IsInsideArea(x, y + 1, area) && filter(area[x, y + 1]) && !neighbors.Contains(area[x, y + 1]))
            {
                neighbors.AddRange(FloodFillArea(x, y + 1, area, filter, neighbors));
            }
            if (IsInsideArea(x, y - 1, area) && filter(area[x, y - 1]) && !neighbors.Contains(area[x, y - 1]))
            {
                neighbors.AddRange(FloodFillArea(x, y - 1, area, filter, neighbors));
            }

            return neighbors;
        }

        private static bool IsInsideArea<T>(int x, int y, T[,] area)
        {
            return x >= 0 && x < area.GetLength(0) && y >= 0 && y < area.GetLength(1);
        }
    }
}
