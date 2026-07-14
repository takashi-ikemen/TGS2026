
public class BindPieceCard : Card
{
    public BindPieceCard()
    {
        name = "BindPieceCard";
    }

    public override GameState Use(GameState state)
    {
        //使用時にフラグを起動
        state.IsBindPieceCardUsed = true;
        return state;
    }

    public override GameState Use(GameState state, int x, int y)
    {
        var newState = state;

        //移動制限をかける駒の座標
        newState.BindX = x;
        newState.BindY = y;


        //カード使用中フラグを切る
        newState.IsBindPieceCardUsed = false;

        //カードを保留から削除
        newState = CardRemover.RemoveCard(newState);

        //ターン交代
        newState.Turn = state.Turn == PieceColor.White ? PieceColor.Black : PieceColor.White;


        return newState;
    }
}
