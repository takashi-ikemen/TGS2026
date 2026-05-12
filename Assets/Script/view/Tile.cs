using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
    public int tileX, tileY;

    Renderer rend;
    [SerializeField] Renderer renderObj;
    Color baseColor;

    public enum HighLightTileType
    {
        None,
        CanArea,
        Hover
    }

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        baseColor = rend.material.color;
        
    }

    public void SetTile(int x, int y)
    {
        this.tileX = x;
        this.tileY = y;
        transform.position = new Vector3(tileX, 0, tileY);
    }

    public void TileHighLight(HighLightTileType type)
    {
        switch(type)
        {
            case HighLightTileType.CanArea:
                rend.material.color = Color.green;
                break;

            case HighLightTileType.Hover:
                rend.material.color = Color.red;
                break;

            default:
                rend.material.color = baseColor;
                break;
        }
    }
}
