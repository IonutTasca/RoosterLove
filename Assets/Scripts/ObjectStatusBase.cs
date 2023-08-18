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
    public event UnityAction OnStatusChange;

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
        OnStatusChange?.Invoke();
    }
    public Status GetStatus() => _status;
}
