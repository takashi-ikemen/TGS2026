using System.Collections.Generic;
using NUnit.Framework;


public class Card
{
    public string name { get; protected set; }//名前は継承先のクラスで設定

    //以下オーバーライド用メソッド
    public  virtual GameState Use(GameState state)
    {
        return state;
    }

    public virtual GameState Use(GameState state, int x, int y)
    {
        return state;
    }

    public virtual GameState Use(GameState state, int x1, int y1, int x2, int y2)
    {
        return state;
    }

}
