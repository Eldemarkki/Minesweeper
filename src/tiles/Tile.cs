namespace Minesweeper
{
    public class Tile
    {
        public Point2D Coordinate { get; set; }
        public bool IsHidden { get; set; }
        public bool IsFlagged { get; set; }

        public Tile(Point2D coordinate)
        {
            Coordinate = coordinate;
            IsHidden = true;
            IsFlagged = false;
        }
    }
}
