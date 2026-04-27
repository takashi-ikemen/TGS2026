public struct Move
{
    public int FromX, FromY;
    public int ToX, ToY;

    public Move(int fx, int fy, int tx, int ty)
    {
        FromX = fx;
        FromY = fy;
        ToX = tx;
        ToY = ty;
    }
}