using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public delegate void GameStart();
    public delegate void ScoreUpdate(int point);
    public delegate void GameOver(int maxScore);

    public static event GameStart onGameStart;
    public static event ScoreUpdate onScoreUpdate;
    public static event GameOver onGameOver;

    public static EventController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void OnGameStart()
    {
        onGameStart?.Invoke();
    }

    public void OnScoreUpdate(int point)
    {
        onScoreUpdate?.Invoke(point);
    }

    public void OnGameOver(int maxScore)
    {
        onGameOver?.Invoke(maxScore);
    }
}
