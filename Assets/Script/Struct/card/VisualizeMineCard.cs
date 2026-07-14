

public class VisualizeMineCard :Card
{
    public VisualizeMineCard()
    {
        name = "VisualizeMineCard";
    }

    public override GameState Use(GameState state)
    {
        var newState = state;

        //地雷と聖杯を視認できるようにする
        newState.Mine.SetIsVisible(true);
        newState.Grail.SetIsVisible(true);

        //カードを保留から削除
        newState = CardRemover.RemoveCard(newState);

        //ターン交代
        newState.Turn = state.Turn == PieceColor.White ? PieceColor.Black : PieceColor.White;

        return newState;
    }
    
}
