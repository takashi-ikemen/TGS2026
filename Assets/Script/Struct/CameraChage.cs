using UnityEngine;

public class CameraChage : MonoBehaviour　　
{
    /* -----
    Cameraを変更するためのスクリプト
    -----*/

    public GameObject mainCamera;
    public GameObject selectCamera;

    private void Update()
    {
        //キーボードを押した際に画面が切り替わる
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (mainCamera.activeSelf)
            {
                mainCamera.SetActive(false);
                selectCamera.SetActive(true);
            }
            else
            {
                mainCamera.SetActive(true);
                selectCamera.SetActive(false);
            }
        }
    }

}
