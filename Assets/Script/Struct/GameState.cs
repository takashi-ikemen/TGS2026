using NUnit.Framework;
using System.Collections.Generic;
public enum Winner { White, Black, none };

public struct GameState
{
    public Board Board;  //盤の情報を格納するBoard
    public PieceColor Turn;  //ターンの情報を返すPieceColor 
    public Mine Mine;  //地雷の情報を返すMine
    public Mine Grail;  //聖杯の情報を返すGrail
    public Winner Winner;  //どちらが勝利したかを保持するWinner
    public int BlackHP;  //Black(黒)のHP
    public int WhiteHP;  //White(白)のHPs

    //カード関連
    public List<Card> DeckCards;
    public List<Card> WhiteCards;
    public List<Card> BlackCards;
    public Card OnHoldWhiteCard;//保留中の白手札
    public Card OnHoldBlackCard;//保留中の黒手札

    //ShieldCard
    public int WhiteShield;//白シールド
    public int BlackShield;//黒シールド

    //GainMineDamageCard
    public int additionalMineDamage; //地雷の追加ダメージ

    //SwitchPieceCard
    public bool IsSwitchPieceCardUsed;

    //UnbreakablePieceCard
    public bool IsBlackUnbreakable;
    public bool IsWhiteUnbreakable;

    //EXTurnCard
    public bool IsBlackEXTurn;
    public bool IsWhiteEXTurn;

    //BindPieceCard
    public bool IsBindPieceCardUsed;
    public int BindX;
    public int BindY;

    //AdditionalMoveCard
    public bool IsAdditionalMoveCardUsed;
    public bool IsAdditionalMovePieceChosen;

    //DetectMineCard
    public bool IsDetectMineCardUsed;
    public bool IsMineDetected;//地雷が見つかったかどうか

    //ForceMoveCard
    public bool IsForceMoveCardUsed;
    public bool IsForceMovePieceChosen;

    //RestrictMoveCard
    public bool IsRestrictMoveCardUsed;
    public bool IsRestrictMovePieceChosen;
    public int RestrictX;
    public int RestrictY;
    public PieceType RestrictPieceType;

    //一時イベント
    public bool TouchObject;
    public bool MineExploded;
    public bool GrailTake;

    public int ExplosionX;  //地雷が爆発した位置を保持(x)
    public int ExplosionY;  //地雷が爆発した位置を保持(y)
}