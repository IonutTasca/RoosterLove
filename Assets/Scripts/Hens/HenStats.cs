using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HenStats : AliveObjectStatsBase
{
    public float idleTimeMin => 5f;
    public float idleTimeMax => 15f;
    public float npcDistanceWalkingThreshHold => 2f;
}
