using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Door Setting")]
    [SerializeField] private Transform doorPivot;
    [SerializeField] private float openAngle = 230f;
    [SerializeField] private float openSpeed = 180f;

    private Quaternion closedRotation;
    private Quaternion openedRotation;

    private bool isOpen;

    public void Initialize()
    {
        Debug.Log("Initialize");
        closedRotation = doorPivot.localRotation;
        /*  openedRotation = Quaternion.Euler(
              doorPivot.localEulerAngles + new Vector3(0f, openAngle, 0f));*/
        //openedRotation = Quaternion.AngleAxis(openAngle, doorPivot.right);
        openedRotation = closedRotation * Quaternion.AngleAxis(openAngle,doorPivot.right);

        //debug
        Debug.Log("Closed : " + closedRotation.eulerAngles);
        Debug.Log("Opened : " + openedRotation.eulerAngles);
    }

    public void Tick()
    {
        Debug.Log("tick");
        if (!isOpen) return;

        Debug.Log("Current : " + doorPivot.localEulerAngles);


        doorPivot.localRotation = Quaternion.RotateTowards(
            doorPivot.localRotation,
            openedRotation,
            openSpeed * Time.deltaTime);
    }

    public void Open()
    {
        Debug.Log("open");
        isOpen = true;
    }
}
