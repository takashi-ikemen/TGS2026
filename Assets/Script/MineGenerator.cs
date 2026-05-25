
using System;

public class MineGenerator
{
    public static GameState GenerateMine(int minX, int maxX, int minY, int maxY,GameState state)
    { 
        //地雷の座標設定
        Random random = new Random();
        int rx, ry,visible;


        //聖杯設置
        Mine grail = new Mine();
        rx = random.Next(minX, maxX);
        ry = random.Next(minY, maxY);
        visible = random.Next(0, 2);
        grail.SetMineX(rx);
        grail.SetMineY(ry);
        grail.SetIsEnable(true);

        //0なら見える、1なら見えない
        if(visible == 0)
        {
            grail.SetIsVisible(true);
        }
        else
        {
            grail.SetIsVisible(false);
        }
        state.Grail = grail;

        //地雷設置
        //聖杯と被らない位置になるまで
        while (true)
        {
            Mine mine = new Mine();
            rx = random.Next(minX, maxX);
            ry = random.Next(minY, maxY);
            if(state.Board.Get(rx,ry).Equals(Piece.Empty) && !(rx == state.Grail.GetMineX() && ry == state.Grail.GetMineY()))
            {
                mine.SetMineX(rx);
                mine.SetMineY(ry);
                mine.SetIsEnable(true);
                if(state.Grail.GetIsVisible() == true)
                {
                    mine.SetIsVisible(false);
                }
                else
                {
                    mine.SetIsVisible(true);
                }
                state.Mine = mine;
                break;
            }
        }

        return state;
    }
    
}
