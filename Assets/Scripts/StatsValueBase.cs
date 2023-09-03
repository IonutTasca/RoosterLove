using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsValueBase
{
    protected virtual string Name { get; }

    private int _value;

    public int Value => _value;

    public void InitializeValue()
    {
        //get value from server
    }
    public void IncreaseValue(int byValue)
    {
        _value += byValue;
        Debug.Log(Name+ ": " + _value); 
    }

    public void DecreaseValue(int byValue)
    {
        if (_value - byValue > 0)
            _value -= byValue;
        else
            _value = 0;
        Debug.Log(Name + ": " + _value);
    }
}
