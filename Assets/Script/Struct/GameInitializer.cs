using JetBrains.Annotations;
using System;
using Unity.VisualScripting;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine;

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

        //ビショップ配置
        board.Set(3, 0, new Piece { Type = PieceType.Bishop, Color = PieceColor.White });
        board.Set(1, 6, new Piece { Type = PieceType.Bishop, Color = PieceColor.Black });

        // ルーク配置
        board.Set(1, 0, new Piece { Type = PieceType.Rook, Color = PieceColor.White });
        board.Set(3, 6, new Piece { Type = PieceType.Rook, Color = PieceColor.Black });

        // ナイト配置
        board.Set(2, 0, new Piece { Type = PieceType.Knight, Color = PieceColor.White });
        board.Set(2, 6, new Piece { Type = PieceType.Knight, Color = PieceColor.Black });

        // キング配置
        for (int x = 1; x <= 3; x++)
        {
            board.Set(x, 1, new Piece { Type = PieceType.King, Color = PieceColor.White });
            board.Set(x, 5, new Piece { Type = PieceType.King, Color = PieceColor.Black });
        }


        GameState state = new GameState
        {
            Board = board,
            Turn = PieceColor.White,
            Winner = Winner.none,
            BlackHP = 6,
            WhiteHP = 6,
            BlackShield = 0,
            WhiteShield = 0,
            additionalMineDamage = 0,
            IsSwitchPieceCardUsed = false,
            IsWhiteUnbreakable = false,
            IsBlackUnbreakable = false,
            IsWhiteEXTurn = false,
            IsBlackEXTurn = false,
            IsBindPieceCardUsed = false,
            IsAdditionalMoveCardUsed = false,
            IsDetectMineCardUsed = false,
            IsMineDetected = false,
            IsForceMoveCardUsed = false,
            IsForceMovePieceChosen = false,
            IsRestrictMoveCardUsed = false,
            IsRestrictMovePieceChosen = false
        };


        //　地雷配置
        //初期配置は0 <= x < 5, 2 <= y < 5

        state = MineGenerator.GenerateMine(0, 5, 2, 5, state);

        //Cardクラスの子クラスをまとめる
        var types = Assembly.GetExecutingAssembly()
        .GetTypes()
        .Where(t => t.IsSubclassOf(typeof(Card)));

        //まとめたCard型の子クラスをList化
        state.DeckCards = new List<Card>();
        state.DeckCards = types.Select(t => (Card)Activator.CreateInstance(t)).ToList();


        //手札初期化
        state.BlackCards = new List<Card>();
        state.WhiteCards = new List<Card>();

        //保留カードの初期化
        state.OnHoldWhiteCard = new Card();
        state.OnHoldBlackCard = new Card();

        return state;

    }

 

}