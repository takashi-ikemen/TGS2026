using UnityEngine;
using UnityEngine.Rendering;

public class TitleUIManager : MonoBehaviour
{
    [SerializeField] GameObject mainPanel;

    [SerializeField] GameObject settingPanel;

    private void Start()
    {
        mainPanel.SetActive(true);
        settingPanel.SetActive(false);
    }

    public void OnClickStart()
    {
        Debug.Log("ÉQÅ[ÉÄäJén");
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