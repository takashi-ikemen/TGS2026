using System;

public class UnbreakablePieceCard : Card
{
    public UnbreakablePieceCard()
    {
        name = "UnbreakablePieceCard";
    }
    public override GameState Use(GameState state)
    {
        var newState = state;
    
        if (state.Turn.Equals(PieceColor.White))    //白のターンなら白破壊不能フラグon
        {
            newState.IsWhiteUnbreakable = true;
        }
        else                                       //黒のターンなら黒破壊不能フラグon
        {
            newState.IsBlackUnbreakable = true;
        }




        //カードを保留から削除
        newState = CardRemover.RemoveCard(newState);

        //ターン交代
        newState.Turn = state.Turn == PieceColor.White ? PieceColor.Black : PieceColor.White;

        return newState;
    }
    
}
