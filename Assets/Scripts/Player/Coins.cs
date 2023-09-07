using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : StatsValueBase
{
    protected override string Name { get => "Coins"; }
    public override void InitializeValue()
    {
        _value = PlayerDataHandler.Instance.CoinsValue;
        OnValueChanged?.Invoke(_value);
    }
    public override void IncreaseValue(int byValue)
    {
        base.IncreaseValue(byValue);
        PlayerDataHandler.Instance.AddCoinsToUser(byValue);
    }
    public override void DecreaseValue(int byValue)
    {
        base.DecreaseValue(byValue);
        PlayerDataHandler.Instance.SubtractCoinsToUser(byValue);
    }
}
