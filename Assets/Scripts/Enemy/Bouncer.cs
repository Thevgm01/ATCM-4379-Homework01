using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bouncer : Enemy
{
    [SerializeField] float _bounceForce = 100;

    protected override void PlayerImpact(Player player)
    {
        base.PlayerImpact(player);
        BallMotor _motor = player.GetComponent<BallMotor>();
        if (_motor != null)
        {
            _motor.Move((player.transform.position - this.transform.position) * _motor.BaseSpeed * 5);
        }
    }
}
