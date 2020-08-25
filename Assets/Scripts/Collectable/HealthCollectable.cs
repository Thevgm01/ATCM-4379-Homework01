using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectable : Collectable
{
    [SerializeField] int _healthIncrease = 1;

    protected override void Collect(Player player)
    {
        player.IncreaseHealth(_healthIncrease);
    }
}
