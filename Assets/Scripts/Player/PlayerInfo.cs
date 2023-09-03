using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : AliveObjectInfoBase
{
    public BodyCount BodyCount { get; private set;}
    public Coins Coins { get; private set; }

    private PlayerLove _playerLove;



    protected override void Start()
    {
        BodyCount = new BodyCount();
        Coins = new Coins();
        base.Start();
        _playerLove = GetComponent<PlayerLove>();

    }

    protected override void InitializeInfos()
    {
        BodyCount.InitializeValue();
        Coins.InitializeValue();
    }
    protected override void OnStatusChange(Status newStatus)
    {
        base.OnStatusChange(newStatus);
        if(newStatus == Status.Loving)
        {
            BodyCount.IncreaseValue(1);
            if (_playerLove.LastHenCoinsValue != 0)
            {
                Coins.IncreaseValue(_playerLove.LastHenCoinsValue);
            }
        }
    }

}
