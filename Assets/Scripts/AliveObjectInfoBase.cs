using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliveObjectInfoBase : MonoBehaviour
{
    public Level Level { get; private set; }
    protected ObjectStatusBase _status;

    protected virtual void Awake()
    {
        Level = new Level();
    }
    protected virtual void Start()
    {
        _status = GetComponent<ObjectStatusBase>();
        _status.OnStatusChange += OnStatusChange;
        

        InitializeInfos();
    }
    protected virtual void OnDestroy()
    {
        if(_status)
            _status.OnStatusChange -= OnStatusChange;
    }
    protected virtual void InitializeInfos()
    {
        Level.InitializeValue();
    }
    protected virtual void OnStatusChange(Status newStatus)
    {
        
    }

}
