using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCount: StatsValueBase
{
    protected override string Name { get => "BodyCount"; }

    public override void InitializeValue()
    {
        _value = PlayerDataHandler.Instance.BodyCountValue;
        OnValueChanged?.Invoke(_value);
    }
    public override void IncreaseValue(int byValue)
    {
        base.IncreaseValue(byValue);
        PlayerDataHandler.Instance.AddBodyCountToUser(byValue);
    }
    public override void DecreaseValue(int byValue)
    {
        base.DecreaseValue(byValue);
        PlayerDataHandler.Instance.SubtractBodyCountToUser(byValue);
    }
}
