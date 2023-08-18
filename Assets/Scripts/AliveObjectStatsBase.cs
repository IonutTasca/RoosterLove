using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliveObjectStats : MonoBehaviour
{
    [SerializeField] protected float _runSpeed = 145f;
    [SerializeField] protected float _walkSpeed = 70f;

    protected const float _turnSmoothTime = 0.05f;

    public float RunSpeed => _runSpeed;
    public float WalkSpeed => _walkSpeed;
    public virtual float TurnSmoothTime => _turnSmoothTime;
}
