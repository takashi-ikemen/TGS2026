using UnityEngine;

public class CameraChage : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject selectCamera;

    private void Update()
    {
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
