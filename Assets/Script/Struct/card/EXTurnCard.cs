using UnityEngine;

public class EXTurnCard : Card
{
    public EXTurnCard()
    {
        name = "EXTurnCard";
    }
    public override GameState Use(GameState state)
    {
        var newState = state;

        //フラグを起動する
        if (state.Turn.Equals(PieceColor.White))
        {
            newState.IsWhiteEXTurn = true;
        }else if (state.Turn.Equals(PieceColor.Black))
        {
            newState.IsBlackEXTurn = true;
        }

        //カードを保留から削除
        newState = CardRemover.RemoveCard(newState);

        //ターン交代
        newState.Turn = state.Turn == PieceColor.White ? PieceColor.Black : PieceColor.White;

        return newState;
    }
}
