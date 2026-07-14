using System.Collections.Generic;

public class CountPieces
{
    public static List<Piece> SurvivePieceCounter(GameState state)　//生存している残りの駒の数をカウント
    {
        var survivePieces = new List<Piece>();
        Piece piece;

        for(int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 7; y++)
            {
                piece = state.Board.Get(x, y);
                if (!piece.IsEmpty)
                {
                    survivePieces.Add(piece);
                }
            }
        }

        return survivePieces;
    }

    public static List<Piece> SurviveBlackPieceCounter(GameState state)  //生存している残りのBlackの駒の数をカウント
    {
        var survivePieces = SurvivePieceCounter(state);
        List<Piece> surviveBlackPieces = new List<Piece>();
        for (int i = 0; i < survivePieces.Count; i++)  //survivePieceの数の分だけくり返す
        {
            if (survivePieces[i].Color == PieceColor.Black)
            {
                surviveBlackPieces.Add(survivePieces[i]);
            }
        }
        return surviveBlackPieces;　　//surviveBlackPiecesはPieceを保持した二次元リスト
    }

    public static List<Piece> SurviveWhitePieceCounter(GameState state)  //生存している残りのWhiteの駒の数をカウント
    {
        var survivePieces = SurvivePieceCounter(state);
        List<Piece> surviveWhitePieces = new List<Piece>();
        for (int i = 0; i < survivePieces.Count; i++)　　//survivePieceの数の分だけくり返す
        {
            if (survivePieces[i].Color == PieceColor.White)
            {
                surviveWhitePieces.Add(survivePieces[i]);
            }
        }
        return surviveWhitePieces;　//surviveWhitePiecesはPieceを保持した二次元リスト
    }

}
