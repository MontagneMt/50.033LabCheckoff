using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>, IPowerupApplicable
{
    // events
    public UnityEvent gameStart;
    public UnityEvent gameRestart;
    public UnityEvent<int> scoreChange;
    public UnityEvent gameOver;
    public UnityEvent<IPowerup> powerupAffectsManager;
    public UnityEvent<IPowerup> powerupAffectsPlayer;

    public IntVariable gameScore;
    private bool paused = false;
    void Start()
    {
        gameStart.Invoke();
        Time.timeScale = 1.0f;
        SceneManager.activeSceneChanged += SceneSetup;
        powerupAffectsManager.AddListener(RequestPowerupEffect);
    }

    public void SceneSetup(Scene cur, Scene next) {
        gameStart.Invoke();
        SetScore(gameScore.Value);
    }

    public void GameRestart()
    {
        // reset score
        gameScore.Value = 0;
        SetScore(gameScore.Value);
        gameRestart.Invoke();
    }

    public void RequestPowerupEffect(IPowerup i)
    {
        i.ApplyPowerup(this);
    }

    public void IncreaseScore(int increment)
    {
        gameScore.ApplyChange(increment);
        SetScore(gameScore.Value);
    }

    public void SetScore(int score)
    {
        scoreChange.Invoke(score);
    }

    public bool isPaused {
        get {
            return paused;
        }
        set {
            paused = value;
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        gameOver.Invoke();
    }
}
