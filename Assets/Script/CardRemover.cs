using UnityEngine;

public class CardRemover
{
    public static GameState RemoveCard(GameState state,string name)
    {
        int i = 0;
        var newState = state;
        if (state.Turn.Equals(PieceColor.White))
        {
            while(i < state.WhiteCards.Count)
            {
                if (state.WhiteCards[i].name.Equals(name))
                {
                    newState.WhiteCards.Remove(state.WhiteCards[i]);
                    break;
                }
                i++;
            }
        }
        else
        {
            while(i < state.BlackCards.Count)
            {
                if (state.BlackCards[i].name.Equals(name))
                {
                    newState.BlackCards.Remove(state.BlackCards[i]);
                    break;
                }
            }
        }

        return newState;
    }
}