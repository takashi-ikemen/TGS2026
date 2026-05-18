using System.Collections.Generic;
using UnityEngine.InputSystem.LowLevel;

public class GameFinisher
{
    public Winner IsGameFinish(GameState state)
    {
        //キングの生存フラグ
        bool isWhiteKingSurvive = false;
        bool isBlackKingSurvive = false;

        //敵コマと自コマの切り分け
        List<Piece> surviveBlackPieces = CountPieces.SurviveBlackPieceCounter(state);
        List<Piece> surviveWhitePieces = CountPieces.SurviveWhitePieceCounter(state);
        
        //キングの生存確認
        for (int i = 0; i < surviveBlackPieces.Count; i++)
        {
            if (surviveBlackPieces[i].Type.Equals(PieceType.King))
            {
                isBlackKingSurvive = true;
            }
        }

        for (int i = 0; i < surviveWhitePieces.Count; i++)
        {
            if (surviveWhitePieces[i].Type.Equals(PieceType.King))
            {
                isWhiteKingSurvive = true;
            }
        }

        //  白敗北　or  黒敗北
        if (surviveWhitePieces.Count == 1 || isWhiteKingSurvive == false)
        {
            return Winner.Black;
        }else if (surviveBlackPieces.Count == 1 || isBlackKingSurvive == false)
        {
            return Winner.White;
        }

            return Winner.none;
    }


}