using JetBrains.Annotations;
using System;
using Unity.VisualScripting;
using System.Collections.Generic;

public static class GameInitializer
{
    public static GameState CreateInitial()
    {
        var board = new Board
        {
            Squares = new Piece[5, 7]
        };

        // 全マスEmptyで初期化
        for (int x = 0; x < 5; x++)
            for (int y = 0; y < 7; y++)
                board.Squares[x, y] = Piece.Empty;

        // ポーン配置
        for (int x = 1; x <= 3; x++)
        {
            board.Set(x, 1, new Piece { Type = PieceType.Pawn, Color = PieceColor.White });
            board.Set(x, 5, new Piece { Type = PieceType.Pawn, Color = PieceColor.Black });
        }

        // ルーク配置
        board.Set(1, 0, new Piece { Type = PieceType.Rook, Color = PieceColor.White });
        board.Set(3, 6, new Piece { Type = PieceType.Rook, Color = PieceColor.Black });

        // ナイト配置
        board.Set(3, 0, new Piece { Type = PieceType.Knight, Color = PieceColor.White });
        board.Set(1, 6, new Piece { Type = PieceType.Knight, Color = PieceColor.Black });

        // キング配置
        board.Set(2, 0, new Piece { Type = PieceType.King, Color = PieceColor.White });
        board.Set(2, 6, new Piece { Type = PieceType.King, Color = PieceColor.Black });


        GameState state = new GameState
        {
            Board = board,
            Turn = PieceColor.White,
            Winner = Winner.none,
            BlackHP = 6,
            WhiteHP = 6,
            BlackShield = 0,
            WhiteShield = 0

        };


        //　地雷配置
        //初期配置は0 <= x < 5, 2 <= y < 5

        state = MineGenerator.GenerateMine(0, 5, 2, 5, state);

        //カード生成
        Card randomHP = new RandomHPCard();
        Card shield = new ShieldCard();
        Card switchPiece = new SwitchPieceCard();

        //カードを山札に追加
        state.DeckCards = new List<Card>();
        state.DeckCards.Add(randomHP);
        state.DeckCards.Add(shield);
        state.DeckCards.Add(switchPiece);

        //手札初期化
        state.BlackCards = new List<Card>();
        state.WhiteCards = new List<Card>();

        return state;

    }

 

}