using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject highscoreText;
    public IntVariable gameScore;

    void Start()
    {
        SetHighscore();
    }

    public void GoToLoadScene()
    {
        SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Single);
    }

    void SetHighscore()
    {
        highscoreText.GetComponent<TextMeshProUGUI>().text = "TOP- " + gameScore.previousHighestValue.ToString("D6");
    }

    public void ResetHighscore()
    {
        GameObject eventSystem = GameObject.Find("EventSystem");
        eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        gameScore.ResetHighestValue();
        SetHighscore();
    }
}