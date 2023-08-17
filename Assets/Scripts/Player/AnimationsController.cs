using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class AnimationsController : MonoBehaviour
{
    private Animator _animator;
    private PlayerStatus _playerStatus;

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();   
        _playerStatus = GetComponent<PlayerStatus>();
    }

    private void Move(bool value)
    {
        _animator.SetBool("moving", value);
        _playerStatus.UpdateStatus(Status.Moving);
    }

 

    public void UpdateSpeed(float value)
    {
        _animator.SetFloat("speed", value);

        if (value <= 0.1f)
            Idle();
        else
            Move(true);

    }
    private void Idle()
    {
        Move(false);
        _playerStatus.UpdateStatus(Status.Idle);

    }
    public void Fly(bool value)
    {
        _animator.SetBool("isFlying", value);
        if(value)
            _playerStatus.UpdateStatus(Status.Flying);
    }

}
