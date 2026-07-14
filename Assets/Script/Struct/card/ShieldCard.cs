using System;
public class ShieldCard : Card
{
    public ShieldCard()
    {
        name = "ShieldCard";
    }
    public override GameState Use(GameState state)
    {
        
        var newState = state;
        if (state.Turn.Equals(PieceColor.White))    //白のターンなら白シールド加算
        {
            newState.WhiteShield = 1;
        }
        else                                       //黒のターンなら黒シールド加算
        {
            newState.BlackShield = 1;
        }


        //カードを保留から削除
        newState = CardRemover.RemoveCard(newState);

        //ターン交代
        newState.Turn = state.Turn == PieceColor.White ? PieceColor.Black : PieceColor.White;

        return newState;


    }
}
