using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public sealed class AnimationsRoosterController : AnimationsControllerBase
{
    public override void Fly(bool value)
    {
        base.Fly(value);

        animator.SetBool("isFlying", value);
        if (value)
            status.UpdateStatus(Status.Flying);
    }


}
