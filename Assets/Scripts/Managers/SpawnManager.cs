
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour, IManager
{
    private float frequency = 1f;
    public int logPool = 15;
    private Vector3[] spawnPoints;
    private Pool pool;
    
    public static SpawnManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        pool = GetComponent<Pool>();
        
        EventController.onGameStart += OnGameStart;
        EventController.onScoreUpdate += OnScoreUpdate;
        EventController.onGameOver += OnGameOver;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnScoreUpdate(int score)
    {
        //frequency = 0.8f;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name =="GameScene")
        {
            spawnPoints = new[]
            {
                GameObject.Find("SpawnPoint1").transform.position,
                GameObject.Find("SpawnPoint2").transform.position,
                GameObject.Find("SpawnPoint3").transform.position
            };
        }
        pool.CreatePool(logPool);
        InvokeSpawning(frequency);
    }

    public void OnGameStart()
    {
        
    }

    private void InvokeSpawning(float freq)
    {
        InvokeRepeating("SpawnLog", 1 / freq, 1 / freq);
    }
    
    private void SpawnLog()
    {
        GameObject obj = pool.Dequeue();
        if (obj == null)
        {
            return;
        }
        
        obj.transform.position = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
        obj.SetActive(true);
        
    }

    public void OnGameOver(int maxScore)
    {
        CancelInvoke("SpawnLog");
    }

    public void Enqueue(GameObject obj)
    {
        pool.Enqueue(obj);
    }
    private void OnDestroy()
    {
        EventController.onGameStart -= OnGameStart;
        EventController.onScoreUpdate -= OnScoreUpdate;
        EventController.onGameOver -= OnGameOver;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
