
using UnityEngine;

public sealed class AnimationsHenController : AnimationsControllerBase
{
    private readonly int _idleAnimationsLength = 3;

    public override void Idle()
    {
        base.Idle();
        int randomIndex = Random.Range(0, _idleAnimationsLength);
        animator.SetInteger("idleIndex", randomIndex);
    }
}
