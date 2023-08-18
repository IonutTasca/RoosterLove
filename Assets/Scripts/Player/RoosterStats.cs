using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class RoosterStats : AliveObjectStats
{
    [SerializeField] private float _flySpeedSlow = 100;
    [SerializeField] private float _flySpeedFast = 200f;
    [SerializeField] private float _flyYPower = 250f;

    public float FlySpeedSlow => _flySpeedSlow;
    public float FlySpeedFast => _flySpeedFast;
    public float FlyYPower => _flyYPower;
}
