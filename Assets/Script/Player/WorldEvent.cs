using UnityEngine;

public abstract class WorldEvent : MonoBehaviour
{
    protected EventController eventController;

    public virtual void Initialize(EventController controller)
    {
        eventController = controller;
    }

    public abstract void Execute();
}
