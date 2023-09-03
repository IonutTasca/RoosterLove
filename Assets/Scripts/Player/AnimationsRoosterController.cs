using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class AnimationsRoosterController : AnimationsControllerBase
{
    public readonly float toFlyTime = 1f;
    public override void Fly(bool value)
    {
        base.Fly(value);

        animator.SetBool("isFlying", value);
        if (value)
            status.UpdateStatus(Status.Flying);
        else
            Idle();
    }


}
