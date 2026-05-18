
using System;

public class MineGenerator
{
    public static Mine GenerateMine(int minX, int maxX, int minY, int maxY,Board board)
    { //’n—‹‚ÌÀ•Wİ’è
        Mine mine = new Mine();
        Random random = new Random();
        int rx, ry;

        while (true)
        {
            rx = random.Next(minX, maxX);
            ry = random.Next(minY, maxY);
            if (board.Get(rx,ry).Equals(Piece.Empty))
            {
                break;
            }
        }

        mine.SetMineX(rx);
        mine.SetMineY(ry);
        return mine;
    }
    
}
