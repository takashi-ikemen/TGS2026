using UnityEngine;

public class CardUser
{
    public  static GameState UseCard(GameState state,string name)
    {
        int i = 0;
        var newState = state;
        if (state.Turn.Equals(PieceColor.White))
        {
            while(i < state.WhiteCards.Count)
            {
                if (state.WhiteCards[i].name.Equals(name))
                {
                    newState = state.WhiteCards[i].Use(state);
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
                    newState = state.BlackCards[i].Use(state);
                    break;
                }
                i++;
            }
        }

        return newState;
    }
    
}
