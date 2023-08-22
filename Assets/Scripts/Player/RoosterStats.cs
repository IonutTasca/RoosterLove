using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class RoosterStats : AliveObjectStatsBase
{
    public override float runSpeed => 3f;

    public override float walkSpeed => 1f;

    public override float turnSmoothTime => 0.05f;

    public float flySpeedSlow => 3f;

    public float flySpeedFast => 6f;

    public float flyYPower => 5f;


}
