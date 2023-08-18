using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public sealed class AnimationsRoosterController : AnimationsControllerBase
{
    public override void Fly(bool value)
    {
        base.Fly(value);

        _animator.SetBool("isFlying", value);
        if (value)
            _status.UpdateStatus(Status.Flying);
    }


}
