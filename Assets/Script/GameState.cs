
public enum Winner { White, Black, none };

public struct GameState
{
    public Board Board;  //盤の情報を格納するBoard
    public PieceColor Turn;  //ターンの情報を返すPieceColor 
    public Mine Mine;  //地雷の情報を返すMine
    public Winner Winner;  //どちらが勝利したかを保持するWinner

    //一時イベント
    public bool MineExploded;

    public int ExplosionX;  //地雷が爆発した位置を保持(x)
    public int ExplosionY;  //地雷が爆発した位置を保持(y)
}