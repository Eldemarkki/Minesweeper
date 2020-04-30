namespace Minesweeper
{
    class BombTile : Tile
    {
        public BombTile(Point2D coordinate) : base(coordinate) 
        {

        }

        public override string ToString()
        {
            return "X";
        }
    }
}
