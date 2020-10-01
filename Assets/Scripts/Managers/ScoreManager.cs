using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour,IScoreManager, ISceneManager
{
    private Text txtScore;
    private GameObject gameOver;
    private int score = 50;
    private int maxScore = 50;
    
    public static ScoreManager instance;

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
        EventController.onScoreUpdate += OnScoreUpdate;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void OnScoreUpdate(int score)
    {
        if (txtScore == null)
        {
            txtScore = GameObject.Find("Score").GetComponent<Text>();
            gameOver = GameObject.Find("GameOver").gameObject;
        }
        
        this.score += score;
        UpdateScoreText(this.score);

        DetermineMaxScoreAndOver(this.score);
    }

    private void DetermineMaxScoreAndOver(int score)
    {
        if (score <= 0)
        {
            EventController.instance.OnGameOver(maxScore);
            txtScore.gameObject.SetActive(false);
            gameOver.SetActive(true);
            gameOver.GetComponent<Text>().text = $"Game Over! \n Max Score : {maxScore}";

        }
        else
        {
            if (score > maxScore)
            {
                maxScore = score;
            }
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            OnScoreUpdate(0); //just to make sure the ui text is not null
        }
    }
    public void UpdateScoreText(int score)
    {
        txtScore.text = $"Score : {score}";
    }
    private void OnDestroy()
    {
        EventController.onScoreUpdate -= OnScoreUpdate;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public int GetCurrentScore()
    {
        return score;
    }
}
