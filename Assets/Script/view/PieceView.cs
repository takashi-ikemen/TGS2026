using UnityEngine;

public class PieceView : MonoBehaviour
{
    public int x, y;

    public void SetPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
        transform.position = new Vector3(x, 0, y);
    }
}