namespace Same_Symbols_Game
{
    public struct Position  // Position x,y on game screen:
    {
        readonly int x, y;
        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int X => x;
        public int Y => y;
    }
}
