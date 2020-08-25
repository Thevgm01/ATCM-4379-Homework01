using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureCollectable : Collectable
{
    [SerializeField] int _treasureIncrease = 50;

    protected override void Collect(Player player)
    {
        player.Treasure += _treasureIncrease;
    }
}
