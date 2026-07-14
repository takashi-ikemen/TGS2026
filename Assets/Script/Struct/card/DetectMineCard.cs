

public class DetectMineCard : Card
{
    public DetectMineCard()
    {
        name = "DetectMineCard";
    }

    public override GameState Use(GameState state)
    {
        state.IsDetectMineCardUsed = true;
        return state;
    }

    public override GameState Use(GameState state, int x, int y)
    {
        var newState = state;
        if (!state.Mine.GetIsVisible())//地雷が見えているか
        {
            if ((state.Mine.GetMineX() <= x + 1 && state.Mine.GetMineX() >= x - 1) &&
                (state.Mine.GetMineY() <= y + 1 && state.Mine.GetMineY() >= y - 1)) //3x3マスに入ってるかどうか
            {
                newState.IsMineDetected = true;
            }
        }
        else if (!state.Grail.GetIsVisible())//聖杯が見えているか
        {
            if((state.Grail.GetMineX() <= x + 1 && state.Grail.GetMineX() >= x - 1) && 
                (state.Grail.GetMineY() <= y + 1 && state.Grail.GetMineY() >= y - 1)) //3x3マスに入ってるかどうか
            {
                newState.IsMineDetected = true;
            }
        }

            //カード使用中フラグを切る
            newState.IsDetectMineCardUsed = false;

        //カードを保留から削除
        newState = CardRemover.RemoveCard(newState);

        //ターン交代
        newState.Turn = state.Turn == PieceColor.White ? PieceColor.Black : PieceColor.White;



        return newState;
    }

}
