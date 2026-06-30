using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera hpCamera;

    public void ShowHpCamera(Transform target)
    {
        mainCamera.enabled = false;
        hpCamera.enabled = true;
    }
    
    public void ShowMainCamera()
    {
        hpCamera.enabled = false;
        mainCamera.enabled = true;
    }
}
