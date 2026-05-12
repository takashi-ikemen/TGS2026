public struct Board
{
    public Piece[,] Squares; // [x, y]

    public Piece Get(int x, int y) => Squares[x, y];
    public void Set(int x, int y, Piece piece) => Squares[x, y] = piece;

    public bool IsInside(int x, int y)
    {
        return x >= 0 && x < 5 && y >= 0 && y < 7;
    }
}