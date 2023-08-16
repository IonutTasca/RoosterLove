using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public enum Status
{
    Moving,
    Flying,
    Loving,
    Idle
}
public class PlayerStatus : MonoBehaviour
{
    public event UnityAction OnStatusChange;

    private Status _status;

    private void Start()
    {
        _status = Status.Idle;
    }
    public void UpdateStatus(Status status)
    {
        if (_status == status)
            return;

        _status = status;
        OnStatusChange?.Invoke();
    }
    public Status GetStatus() => _status;
   
}
