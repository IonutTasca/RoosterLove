using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationsControllerBase : MonoBehaviour
{
    protected Animator animator;
    protected ObjectStatusBase status;

    protected readonly float loveTime = 2f;

    public event UnityAction OnLoveEnded;
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        status = GetComponent<ObjectStatusBase>(); 
    }

    private void Move(bool value)
    {
        animator.SetBool("moving", value);
        if(value)
            status.UpdateStatus(Status.Moving);
    }

    public void MakeLove()
    {
        animator.SetTrigger("love");
        status.UpdateStatus(Status.Loving);
        Invoke(nameof(StopLove),loveTime);    
    }
  
    protected void StopLove()
    {
        animator.ResetTrigger("love");
        Idle();
        OnLoveEnded?.Invoke();
    }
    public virtual void UpdateSpeed(float value)
    {
        animator.SetFloat("speed", value);

        if (value <= 0.1f)
            Idle();
        else
            Move(true);

    }
    public virtual void Idle()
    {
        Move(false);
        status.UpdateStatus(Status.Idle);

    }
    public virtual void Fly(bool value)
    {
        
    }
}
