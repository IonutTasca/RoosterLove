using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HenInfo : AliveObjectInfoBase
{

    private int _timesLovedToUpdate = 3;
    private int _currentTimesLoved = 0;

    public int CoinsValue { get; private set; }

    protected override void InitializeInfos()
    {
        base.InitializeInfos();
        _timesLovedToUpdate = 3;/// get this from DB
        CoinsValue = 5;//get form DB
    }
    protected override void OnStatusChange(Status newStatus)
    {
        if (newStatus == Status.Loving)
        {
            _currentTimesLoved++;
            if(_currentTimesLoved >= _timesLovedToUpdate)
            {
                //level down hen
                Level.DecreaseValue(1);

            }
            else
            {
                //update ui for times loved
            }
        }
    }
}
