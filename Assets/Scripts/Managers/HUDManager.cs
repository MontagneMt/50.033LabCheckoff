using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    // 0 for normal mode, 1 for gameover
    private Vector3[] scoreTextPosition = {
        new Vector3(-820, 476, 0),
        new Vector3(-91, 57, 0)
    };
    // 0 for normal mode, 1 for gameover
    private Vector3[] restartButtonPosition = {
        new Vector3(856, 469, 0),
        new Vector3(-3, -224, 0)
    };
    private Vector3[] scoreTextScale = {
        new Vector3(1, 1, 0),
        new Vector3(1.5f, 1.5f, 0)
    };
    private Vector3[] restartButtonScale = {
        new Vector3(1, 1,0),
        new Vector3(1.5f, 1.5f, 0)
    };

    public GameObject scoreText;
    public Transform restartButton;
    public GameObject gameOverCanvas;
    public GameObject highscoreText;
    public IntVariable gameScore;
    public GameObject pauseButton;

    void Awake()
    {
        GameManager.instance.gameStart.AddListener(GameStart);
        GameManager.instance.gameOver.AddListener(GameOver);
        GameManager.instance.gameRestart.AddListener(GameStart);
        GameManager.instance.scoreChange.AddListener(SetScore);
    }

    void Start()
    {
        gameOverCanvas.SetActive(false);
    }

    public void GameStart()
    {
        gameOverCanvas.SetActive(false);
        scoreText.transform.localPosition = scoreTextPosition[0];
        scoreText.transform.localScale = scoreTextScale[0];
        restartButton.localPosition = restartButtonPosition[0];
        restartButton.localScale = restartButtonScale[0];
    }

    public void SetScore(int score)
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
    }

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
        pauseButton.SetActive(false);
        scoreText.transform.localPosition = scoreTextPosition[1];
        scoreText.transform.localScale = scoreTextScale[1];
        restartButton.localPosition = restartButtonPosition[1];
        restartButton.localScale = restartButtonScale[1];
        highscoreText.GetComponent<TextMeshProUGUI>().text = "TOP- " + gameScore.previousHighestValue.ToString("D6");
        // show
        highscoreText.SetActive(true);
    }
}
