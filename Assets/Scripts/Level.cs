using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Level : StatsValueBase
{
    protected override string Name { get =>  "Level"; }
    public override void InitializeValue()
    {
        _value = PlayerDataHandler.Instance.BodyCountValue;
        OnValueChanged?.Invoke(_value);
    }
}
