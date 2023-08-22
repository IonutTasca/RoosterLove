using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AliveObjectStatsBase: MonoBehaviour
{
    public abstract float runSpeed { get; }
    public abstract float walkSpeed { get; }
    public abstract float turnSmoothTime { get; }
}
