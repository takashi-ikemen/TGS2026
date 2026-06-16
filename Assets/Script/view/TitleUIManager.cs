using UnityEngine;
using UnityEngine.Rendering;

public class TitleUIManager : MonoBehaviour
{
    [SerializeField] GameObject mainPanel;

    [SerializeField] GameObject settingPanel;

    [SerializeField] SceneController sceneController;

    private void Start()
    {
        mainPanel.SetActive(true);
        settingPanel.SetActive(false);
    }

    public void OnClickStart()
    {
        Debug.Log("ゲーム開始");
        sceneController.SceneChange("GameScene");

    }

    public void OnClickRetry()
    {
        Debug.Log("タイトルに戻る");
        sceneController.SceneChange("StartScene");
    }

    public void OnClickSetting()
    {
        mainPanel.SetActive(false);
        settingPanel.SetActive(true);
    }

    public void OnClickBack()
    {
        settingPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void OnClickEnd()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}