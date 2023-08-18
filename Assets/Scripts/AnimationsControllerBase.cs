using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsControllerBase : MonoBehaviour
{
    protected Animator _animator;
    protected ObjectStatusBase _status;

    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _status = GetComponent<ObjectStatusBase>(); 
    }

    private void Move(bool value)
    {
        _animator.SetBool("moving", value);
        _status.UpdateStatus(Status.Moving);
    }



    public virtual void UpdateSpeed(float value)
    {
        _animator.SetFloat("speed", value);

        if (value <= 0.1f)
            Idle();
        else
            Move(true);

    }
    public virtual void Idle()
    {
        Move(false);
        _status.UpdateStatus(Status.Idle);

    }
    public virtual void Fly(bool value)
    {
        
    }
}
