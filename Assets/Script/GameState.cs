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

    //シールド
    public int WhiteShield;
    public int BlackShield;

    //一時イベント
    public bool TouchObject;
    public bool MineExploded;
    public bool GrailTake;

    public int ExplosionX;  //地雷が爆発した位置を保持(x)
    public int ExplosionY;  //地雷が爆発した位置を保持(y)
}