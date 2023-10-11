using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverCanvas;

    private void Start()
    {
        
        gameOverCanvas.SetActive(false);
    }

    public void ShowGameOverScreen()
    {
       
        gameOverCanvas.SetActive(true);
    }

    public void RestartGame()
    {
        
    }
}
