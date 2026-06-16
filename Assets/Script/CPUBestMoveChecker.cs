/*using UnityEngine;
using System.Collections.Generic;
using JetBrains.Annotations;

public class CPUBestMoveChecker
{
    public GameState tmpState;
    private int score = 0;

    public Move BestCheck(GameState state , int score)
    {
        //int nowscore = score;
        
        tmpState = state;
        List<Move> moves = MoveGenerator.GenerateMoves(tmpState);
        foreach (var item in moves)
        {
            if(item.ToX == state.Grail.GetMineX() && item.ToY == state.Grail.GetMineY())
            {
                break;
            }

            BestCheck(tmpState, score + 10);
            
        }
    }
}
*/