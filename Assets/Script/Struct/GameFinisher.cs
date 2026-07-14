using System.Collections.Generic;
using UnityEngine.InputSystem.LowLevel;

public class GameFinisher
{
    public Winner IsGameFinish(GameState state)
    {
        //‘Šè‚ÌHP‚ğ0‚É‚µ‚½‚çŸ‚¿
        if(state.WhiteHP <= 0)
        {
            return Winner.Black;
        }else if(state.BlackHP <= 0)
        {
            return Winner.White;
        }

        return Winner.none;
    }


}