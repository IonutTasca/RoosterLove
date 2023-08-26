using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AliveObjectStatsBase: MonoBehaviour
{
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _turnSmoothTime;

    public float CurrentSpeed { get; set; }

    public float RunSpeed => _runSpeed;
    public float WalkSpeed => _walkSpeed;
    public float TurnSmoothTime => _turnSmoothTime;
}
