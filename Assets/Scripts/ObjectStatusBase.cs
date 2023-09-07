using UnityEngine;
using UnityEngine.Events;

public enum Status
{
    Moving,
    Flying,
    Loving,
    Idle
}

public class ObjectStatusBase : MonoBehaviour
{
    public event UnityAction<Status> OnStatusChange;

    private Status _status;

    protected virtual void Start()
    {
        _status = Status.Idle;
    }
    public virtual void UpdateStatus(Status status)
    {
        if (_status == status)
            return;

        _status = status;
        OnStatusChange?.Invoke(status);
    }
    public Status GetStatus() => _status;
}
