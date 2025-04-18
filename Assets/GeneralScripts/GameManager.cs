using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    //float maxRounds = 20;
    private float currentRound = 0;
    public AudioSource audioSource;

    EnemyManager enemyManager;

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

    private void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
        PlayMusic();
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
           if (currentRound <= 20)
            {
                GoToNextRound();
            } 
            else
            {
                WinGame();
            }
            
        }
    }

    void GoToNextRound()
    {
        currentRound += 1;
    }

    public void PlayMusic()
    {
        audioSource.Play();
    }

    public void StopPlayingMusic()
    {
        audioSource.Stop();
    }

    void WinGame()
    {
        SceneManager.LoadScene("Win Screen");
    }
}
