using UnityEngine;
using UnityEngine.InputSystem;
public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform CameraPivot;
    [SerializeField] private float sensitivity = 150f;

    private Vector2 lookInput;
    private float pitch;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        float mouseX = lookInput.x * sensitivity * Time.deltaTime;
        float mouseY = lookInput.y * sensitivity * Time.deltaTime;

        //ç∂âEÇÕPlayerÇâÒì]
        player.Rotate(Vector3.up * mouseX);

        //è„â∫ÇÕCameraPivotÇæÇØâÒì]
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -80f, 80f);


        transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}
