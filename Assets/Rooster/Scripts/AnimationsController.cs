using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class AnimationsController : MonoBehaviour
{
    private Animator _animator;
    void Start()
    {
     _animator = GetComponent<Animator>();   
    }

    private void Move(bool value)
    {
        _animator.SetBool("moving", value);
    }

 

    public void UpdateSpeed(float value)
    {
        _animator.SetFloat("speed", value);

        if (value == 0)
            Idle();
        else
            Move(true);

    }
    private void Idle()
    {
        Move(false);
    }
    public void Fly(bool value)
    {
        _animator.SetBool("isFlying", value);
    }

}
