
public enum Winner { White, Black, none };

public struct GameState
{
    public Board Board;
    public PieceColor Turn;
    public Mine Mine;
    public Winner Winner;

    //ˆêŽžƒCƒxƒ“ƒg
    public bool MineExploded;

    public int ExplosionX;
    public int ExplosionY;
}