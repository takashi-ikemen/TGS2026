using System;

public class CardDrawer
{
    public GameState DrawCard(GameState state)
    {
        var newState = state;
        Random random = new Random();
        int drawIdx = random.Next(0, state.DeckCards.Count);//引くカードをランダムに設定

        //デッキにカードがあるなら引く
        if(state.DeckCards.Count != 0)
        {
            if (state.Turn.Equals(PieceColor.White))
            {
                //白の手札に追加
                newState.WhiteCards.Add(state.DeckCards[drawIdx]);
            }
            else
            {
                //黒の手札に追加
                newState.BlackCards.Add(state.DeckCards[drawIdx]);
            }
            newState.DeckCards.Remove(state.DeckCards[drawIdx]);
        }

        return newState;
    }
}
