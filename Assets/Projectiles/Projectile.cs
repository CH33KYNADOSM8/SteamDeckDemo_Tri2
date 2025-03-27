using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IPooledObject
{
    // Create reference to the rigidbody component on the projectile, and create a variable for the damage it will deal
    public Rigidbody Rigidbody;
    [SerializeField] float damageOnImpact = 10;

    // Get a reference to the object pool that the projectiles will be in.
    public ObjectPool Pool { get; set; }


    /*
     * Check to see if the projectile has collided with anything in the scene. 
     * 
     * If the object is an enemy, call the Take Damage function from the EnemyBase script so they can lose health.
     * 
     * Return to projectile back to the pool after the collision has take place.
     */
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyBase enemy))
        {
            enemy.TakeDamage(damageOnImpact);
        }

        Pool.ReturnObject(this);
    }
}