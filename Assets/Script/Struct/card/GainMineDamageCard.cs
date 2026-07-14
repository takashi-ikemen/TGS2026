using NUnit.Framework;
using System;

public class GainMineDamageCard : Card
{
    public GainMineDamageCard()
    {
        name = "GainMineDamageCard";
    }

    public override GameState Use(GameState state)
    {
        var newState = state;

        //増加分のダメージ
        newState.additionalMineDamage = 1;

        //カードを保留から削除
        newState = CardRemover.RemoveCard(newState);

        //ターン交代
        newState.Turn = state.Turn == PieceColor.White ? PieceColor.Black : PieceColor.White;


        return newState;
    }

}
