using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCollectable : Collectable
{
    [SerializeField] int _speedIncrease = 5;

    protected override void Collect(Player player)
    {
        BallMotor _motor = player.GetComponent<BallMotor>();
        if(_motor != null)
        {
            _motor.MaxSpeed += _speedIncrease;
        }
    }

    protected override void Movement(Rigidbody rb)
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(MovementSpeed, MovementSpeed, MovementSpeed));
    }
}
