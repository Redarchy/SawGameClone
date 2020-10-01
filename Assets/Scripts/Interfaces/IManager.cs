using UnityEngine.SceneManagement;

interface IManager : IScoreManager, ISceneManager
{
    void OnGameStart();
    void OnGameOver(int maxScore);
}

interface IScoreManager
{
    void OnScoreUpdate(int score);
}

interface ISceneManager
{
    void OnSceneLoaded(Scene scene, LoadSceneMode mode);
}