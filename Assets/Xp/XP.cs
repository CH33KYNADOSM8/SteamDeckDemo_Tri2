using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XP : MonoBehaviour, IPooledObject
{

    // Get a reference to the object pool
    public ObjectPool Pool { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }


    /*
     * Check to see if the XP has been collided with.
     * 
     * If it has, add 10 to the player xp counter and destroy the object after.
     */ 
    private void OnTriggerEnter(Collider collider)
    {
            PlayerStats.Instance.playerXp += 10;
            print(PlayerStats.Instance.playerXp);
            Destroy(gameObject);
    }
}
