using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : Pickup
{
    public SpeedPowerup powerup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        // variable to store other object's PowerupManager - if it has one
        PowerupManager powerupManager = other.GetComponent<PowerupManager>();

        // If the other object has a PowerupManager
        if (powerupManager != null)
        {
            //Add the powerup
            powerupManager.Add(powerup);

            //Destroy this pickup
            Destroy(gameObject);
        }
    }
}
