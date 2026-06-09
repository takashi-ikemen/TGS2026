using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject R_tilePrefab;
    public GameObject B_tilePrefab;

    Tile[,] tiles = new Tile[5, 7];


    public void Initialize()
    {
        for (int x = 0; x < 5; x++)
            for (int y = 0; y < 7; y++)
            {
                var obj = ((x + y) % 2 == 0) ? R_tilePrefab : B_tilePrefab;

                var instance = Instantiate(obj);
                var tileObj = instance.GetComponent<Tile>();
                tileObj.SetTile(x, y);

                tiles[x, y] = tileObj;
            }
    }


    /// <summary>
    /// 動けるエリアのタイルの色を変更
    /// </summary>
    public void ViewArea(int fx, int fy)
    {
        ClearHighLight();

        GameManager manager = gameManager;

        List<Move> area = manager.ViewCanMoveArea(fx, fy);
        foreach (var item in area)
        {
            tiles[item.ToX, item.ToY].TileHighLight(Tile.HighLightTileType.CanArea);


        }
    }

    /// <summary>
    /// タイルの色をデフォルトに戻す
    /// </summary>
    public void ClearHighLight()
    {
        for (int x = 0; x < 5; x++)
            for (int y = 0; y < 7; y++)
            {
                tiles[x, y].TileHighLight(Tile.HighLightTileType.None);
            }
    }
}