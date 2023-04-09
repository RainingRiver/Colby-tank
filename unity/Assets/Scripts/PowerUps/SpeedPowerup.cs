using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class SpeedPowerup : Powerup
{
    public float speedToAdd;
    public override void Apply(PowerupManager target)
    {
       Pawn targetSpeed = target.GetComponent<Pawn>();

        if (targetSpeed != null) 
        { 
            targetSpeed.moveSpeed += speedToAdd;
        }
    }

    public override void Remove(PowerupManager target)
    {
        Pawn targetSpeed= target.GetComponent<Pawn>();

        if (targetSpeed != null)
        {
            targetSpeed.moveSpeed -= speedToAdd;
        }
    }
}
