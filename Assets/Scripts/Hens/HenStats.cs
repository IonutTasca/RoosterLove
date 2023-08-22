using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HenStats : AliveObjectStatsBase
{
    public override float runSpeed => 2f;
    public override float walkSpeed => 0.2f;
    public override float turnSmoothTime => 6.5f;

    public float idleTimeMin => 5f;
    public float idleTimeMax => 15f;
    public float npcDistanceWalkingThreshHold => 2f;
}
