using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliveObjectStats : MonoBehaviour
{
    private float _runSpeed = 3f;
    private float _walkSpeed = 1f;

    protected const float _turnSmoothTime = 0.05f;

    public virtual float RunSpeed => _runSpeed;
    public virtual float WalkSpeed => _walkSpeed;
    public virtual float TurnSmoothTime => _turnSmoothTime;
}
