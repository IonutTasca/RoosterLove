using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class RoosterStats : AliveObjectStatsBase
{
    [SerializeField] private float _flySpeedSlow;
    [SerializeField] private float _flySpeedFast;
    [SerializeField] private float _flyYPower;

    public float FlySpeedSlow => _flySpeedSlow;
    public float FlySpeedFast => _flySpeedFast;
    public float FlyYPower => _flyYPower;

}
