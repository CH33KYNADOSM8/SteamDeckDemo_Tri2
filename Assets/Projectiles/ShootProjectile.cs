using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    // Create the variables for the projectiles mechanics.
    [SerializeField] Transform firePoint;
    public ObjectPool projectilePool;                                 // Reference to the object inside of the projectile pool
    public float shootSpeed = 20f;                                    // Determines the speed at which the object travels

    private float nextTimeToShoot = 0;                                // Variable for the countdown before player is allowed to shoot again
    private float fireRate = 1;                                       // Variable to add to the timer before the player is allowed to shoot again

    /*
     * Check to see if the timer has run out so the player can shoot again, if not, do nothing.
     * 
     * Get a reference to the projectile script so I can set the projectile in that to be the one inside of the object pool.
     * 
     * Check to see if the firebolt reference has been gotten.
     * 
     * We add the fire rate variable to the timer variable. The time on this will constantly update meaning that whenever the shoot button
     * is pressed, 1 will be added to it, and the player wont be able to shoot again until the 'Time.time' is a greater number than the 
     * time to shoot variable, so 1 second.
     * 
     * Then cerate the spawn position of the object (in front of the player).
     * Add a veclocity to the object so it moves at the rate of the shoot speed variable.
     */ 
    public void Spawn()
    {
        if (nextTimeToShoot > Time.time)
            return;

        Projectile firebolt = projectilePool.GetObject<Projectile>();   

        if (firebolt == null)
        {
            return;
        }

        nextTimeToShoot = Time.time + fireRate;

        Vector3 spawnPosition = firePoint.position + firePoint.forward;

        firebolt.transform.position = spawnPosition;

        firebolt.Rigidbody.velocity = firePoint.forward * shootSpeed;
    }
}