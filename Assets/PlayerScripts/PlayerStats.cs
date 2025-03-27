using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Create an instance to the player so I can reference the players stats (health) in other scripts.
    public static PlayerStats Instance {get; private set;}

    public float playerHealth = 100;
    public float playerXp = 0;

    float playerMaxXp = 100;

    // Make sure there is only one instance in the scene.
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Check to see if the players health is above 0. If it is, they are alive, if not, kill them by destroying the player character.
    private void Update()
    {
        if (playerHealth <= 0)
        {
            Destroy(gameObject);
        }

        IncreaseXpAmountNeeded();
    }

    void IncreaseXpAmountNeeded()
    {
        if (playerXp >= playerMaxXp)
        {
            playerXp = 0;
            playerMaxXp += 50;
        }
    }
}
