using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public bool isWhiteWin;
    /// <summary>
    /// ƒV[ƒ“‘JˆÚ
    /// </summary>
    public void SceneChange(string _loadScene)
    {
        SceneManager.LoadScene(_loadScene);
    }


}
