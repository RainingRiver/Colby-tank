using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    [SerializeField]
    public List<Powerup> powerups;
    public List<Powerup> removedPowerupQueue;

    // Start is called before the first frame update
    void Start()
    {
        powerups = new List<Powerup>();
        removedPowerupQueue = new List<Powerup>();
    }

    // Update is called once per frame
    void Update()
    {
        DecrementPowerupTimes();
    }

    private void LateUpdate()
    {
        ApplyRomovePowerupsQueue();
    }

    // The Add function will eventually add a powerup
    public void Add(Powerup powerupToAdd)
    {
        powerupToAdd.Apply(this);

        powerups.Add(powerupToAdd);
    }

    // The Add function will eventually add a powerup
    public void Remove(Powerup powerupToRemove)
    {
        removedPowerupQueue.Add(powerupToRemove);
        powerupToRemove.Remove(this);
    }

    private void ApplyRomovePowerupsQueue()
    {
        foreach(Powerup powerup in removedPowerupQueue)
        {
            powerups.Remove(powerup);
        }
    }

    public void DecrementPowerupTimes()
    {
        foreach(Powerup powerupToRemove in powerups) 
        { 
            powerupToRemove.duration -= Time.deltaTime;

            if(powerupToRemove.duration <= 0)
            {
                Remove(powerupToRemove);
            }
        }
    }
}
