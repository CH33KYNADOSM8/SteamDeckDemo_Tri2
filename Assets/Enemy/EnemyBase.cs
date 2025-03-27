using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public GameObject xp;

    [SerializeField] private float maxHealth = 50;
    private float currentHealth;

    [SerializeField] float attackDamage = 10;
    float enemyAttackTimer = 1;
    bool canEnemyAttack = true;

    Vector3 playerPosition => Movement.Instance.transform.position;


    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        /*
         * Check to see if the enemy can attack (bool = false) and the timer has time left (timer > 0).
         * If this is the case, call the method to set the bool to true so the enemy can then attack the player.
         * If this isn't true, set the timer back to 1 second.
         */
        if (!canEnemyAttack && enemyAttackTimer > 0)
        {
            EnemyAttackCountdown();
        }
        else
        {
            enemyAttackTimer = 1;
        }

        /*
         * If the boolean is true, the enemy can attack the player and therefore call the attack player function so the player will take damage from the enemy.
         */
        if (canEnemyAttack)
        {
            AttackPlayer();
        }
    }



    /*
     * This will obtain the enemy and player positions and find the distance between them both.
     * This is so I can determine whether or not the enemy is close enough to attack the player.
     */
    private float CheckDistanceFromPlayer()
    {
        Vector3 enemyPosition = transform.position;

        float distanceBetweenEnemyAndPlayer = Vector3.Distance(enemyPosition, playerPosition);

        return distanceBetweenEnemyAndPlayer;
    }



    /*
     * Check to see if the distance between the enemy and player is less than or equal to 2.
     * If it is, take enemies attack damage away from the player health and turn enemy attack boolean to false.
     * This is so the player doesn't die straight away. Instead there will be a timer in between each attack.
     */
    private bool AttackPlayer()
    {
        if (CheckDistanceFromPlayer() <= 2)
        {
            PlayerStats.Instance.playerHealth -= attackDamage;
            canEnemyAttack = false;
        }

        return canEnemyAttack;
    }



    /*
     * This creates the timer for the enemy attacks.
     * Once the timer has reached 0 seconds, turn the can enemy attack boolean to true, allowing the enemy to deal damage to the player again.
     */
    private bool EnemyAttackCountdown()
    {
        enemyAttackTimer -= Time.deltaTime;

        if (enemyAttackTimer <= 0)
        {
            canEnemyAttack = true;
        }

        return canEnemyAttack;
    }

    /*
     * Get a random amount of XP that will drop when the enemy dies.
     * Obtain the last position of the enemy once it has been killed.
     * 
     * Create a for loop for the same amount of XP that will have been dropped. 
     * Create some new variables to add to the position of the XP so it appears as though the XP has been scattered and isn't all in one 
     * place.
     * 
     * Make sure that the position of the XP is within the world bounds.
     */ 
    void DropXp()
    {
        float randomAmountXpDropped = Random.Range(1, 5);

        Vector3 lastEnemyPosition = new Vector3(transform.position.x, 0.7f, transform.position.z);

        for (int i = 0; i < randomAmountXpDropped; i++)
        {
            float randomDropPositionX = Random.Range(-2, 2);
            float randomDropPositionZ = Random.Range(-2, 2);

            lastEnemyPosition.x += randomDropPositionX;
            lastEnemyPosition.z += randomDropPositionZ;


            // Update this
            if (lastEnemyPosition.x < -18)
            {
                lastEnemyPosition.x = -18;
            }
            else if (lastEnemyPosition.x > 19)
            {
                lastEnemyPosition.x = 19;
            }

            if (lastEnemyPosition.z < -24)
            {
                lastEnemyPosition.z = -23.5f;
            }
            else if(lastEnemyPosition.z > 25)
            {
                lastEnemyPosition.z = 24;
            }

            Instantiate(xp, lastEnemyPosition, Quaternion.identity);
        }
    }

    /*
     * Decrease the enemies health when the player attacks it.
     * 
     * Check to see if the enemy health is 0. If it is, create the XP in that position and destroy the enemy object.
     */
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            DropXp();

            EnemyManager.OnEnemyKilled(this);

            Destroy(gameObject);
        }
    }
}