using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public GameObject target;
    public int height = 20;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TargetPlayerOne();
        if (target != null)
        {
            gameObject.transform.position = target.transform.position + (Vector3.up * height);
        }
    }

    protected void TargetPlayerOne()
    {
        // If the GameManager exists
        if (GameManager.instance != null)
        {
            // And there are players in it
            if (GameManager.instance.players.Count > 0)
            {
                if (GameManager.instance.players[0].pawn.gameObject != null)
                    // Then target the gameObject of the pawn of the first player controller in the list
                    target = GameManager.instance.players[0].pawn.gameObject;
            }
        }
    }


}
