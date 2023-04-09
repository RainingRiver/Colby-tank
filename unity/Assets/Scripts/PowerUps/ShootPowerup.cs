using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class ShootPowerup : Powerup
{
    public float cooldownDecrease;

    public override void Apply(PowerupManager target)
    {
        TankPawn targetCoolDown = target.GetComponent<TankPawn>();

        if (targetCoolDown != null)
        {
            targetCoolDown.fireCoolDown -= cooldownDecrease;
            targetCoolDown.fireCoolDown = Mathf.Clamp(targetCoolDown.fireCoolDown, 0, 1.5f);
        }
    }

    public override void Remove(PowerupManager target)
    {

    }
}
