using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HenStats : AliveObjectStats
{
    private float _runSpeed = 2f;
    private float _walkSpeed = 0.2f;

    private float _idleTimeMin = 5f;
    private float _idleTimeMax = 15f;
    private new const float _turnSmoothTime = 6.5f;

    private float _npcDistanceWalkingThreshHold = 2f;
    public float IdleTimeMin => _idleTimeMin;
    public float IdleTimeMax => _idleTimeMax;
    public override float TurnSmoothTime => _turnSmoothTime;
    public float NpcDistanceWalkingThreshHold => _npcDistanceWalkingThreshHold;
    public override float RunSpeed => _runSpeed;
    public override float WalkSpeed => _walkSpeed;
}
