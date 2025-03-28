using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{ 
    // Reference to the enemy script that shares common functionality.
    // Create a list to add enemies to.
    public EnemyBase enemy;

    private List<EnemyBase> enemyList = new List<EnemyBase>();                   // List to add enemies too when they are spawned into the world

    [SerializeField] List<Wave> waves = new List<Wave>();

    public static Action<EnemyBase> OnEnemyKilled;

    //Create enemies at the start of the game.
    private void Awake()
    {
        CreateEnemy();
    }

    private void OnEnable()
    {
        OnEnemyKilled += EnemyDeath;
    }

    private void OnDisable()
    {
        OnEnemyKilled -= EnemyDeath;
    }


    void EnemyDeath(EnemyBase enemy)
    {
        enemyList.Remove(enemy);
    }

    /*
     * Use a for loop to instantiate a certain number of enemies.
     * 
     * Create random coordinates for the X and Z axis inside of the world barriers so the enemies won't spawn in one place.
     * 
     * Instantiate the enemies using the enemy prefab and the vector position that was just created.
     * Add the enemies to the list so we can keep track of how many are left inside of the game world.
     */ 

    void SpawnWave(Wave wave)
    {
        for (int i = 0; i < wave.EnemyCount; i++)
        {
            CreateEnemy();
        }
    }
    public void CreateEnemy()
    {
       //for (int i = 0; i < 3; i++)
       //{
            float randomZCoordinates = Random.Range(-13, 23);
            float randomXCoordinates = Random.Range(-18, 19);
            Vector3 randomPosition = new Vector3(randomXCoordinates, 1, randomZCoordinates);

            EnemyBase enemyInstance = Instantiate(enemy, randomPosition, Quaternion.identity);

            enemyList.Add(enemyInstance);
       //}
    }

    public float GetEnemyListCount()
    {
        return enemyList.Count;
    }
}