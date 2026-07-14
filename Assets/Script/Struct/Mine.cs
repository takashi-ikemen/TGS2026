using UnityEngine.Rendering;

public struct Mine
{
    // ’n—‹‚ÌÀ•W
    int MineX;
    int MineY;
    bool isEnable;
    bool isVisible;

    public int GetMineX() => MineX;
    public void SetMineX(int nx) => MineX = nx;
    public int GetMineY() => MineY;
    public void SetMineY(int ny) => MineY = ny;
    public bool GetIsEnable() => isEnable;
    public void SetIsEnable(bool nIsEnable) => isEnable = nIsEnable;
    public bool GetIsVisible() => isVisible;
    public void SetIsVisible(bool nIsVisible) => isVisible = nIsVisible;

}
