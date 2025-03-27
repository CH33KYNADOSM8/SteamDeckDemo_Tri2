using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //float maxRounds = 20;
    float currentRound = 0;

    EnemyManager enemyManager;

    private void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
    }
    private void OnEnable()
    {
        EnemyManager.OnEnemyKilled += OnEnemyKilled;
    }

    private void OnDisable()
    {
        EnemyManager.OnEnemyKilled -= OnEnemyKilled;
    }


    void OnEnemyKilled(EnemyBase enemy)
    {
        if (enemyManager.GetEnemyListCount() == 0)
        {
            GoToNextRound();
        }
    }

    void GoToNextRound()
    {
        currentRound += 1;
    }
}
