using UnityEngine;

public class ShuffleMineCard : Card
{
    public ShuffleMineCard()
    {
        name = "ShuffleMineCard";
    }
    public override GameState Use(GameState state)
    {
        var newState = state;

        //地雷シャッフル
        newState = MineGenerator.GenerateMine(0, 5, 0, 7, state);

        //カードを保留から削除
        newState = CardRemover.RemoveCard(newState);

        //ターン交代
        newState.Turn = state.Turn == PieceColor.White ? PieceColor.Black : PieceColor.White;



        return newState;
    }
}
