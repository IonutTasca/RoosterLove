using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoosterStats : MonoBehaviour
{
    [SerializeField] private float _runSpeed = 145f;
    [SerializeField] private float _walkSpeed = 70f;
    [SerializeField] private float _flySpeedSlow = 100;
    [SerializeField] private float _flySpeedFast = 200f;
    [SerializeField] private float _flyYPower = 250f;

    private const float _turnSmoothTime = 0.05f;

    public float RunSpeed => _runSpeed;
    public float WalkSpeed => _walkSpeed;
    public float FlySpeedSlow => _flySpeedSlow;
    public float FlySpeedFast => _flySpeedFast;
    public float FlyYPower => _flyYPower;
    public float TurnSmoothTime => _turnSmoothTime;
}
