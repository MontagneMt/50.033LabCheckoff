using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator animator;
    private Scene sceneToLoad;

    public void LoadScene(Scene scene)
    {
        sceneToLoad = scene;
        Time.timeScale = 0f;
        animator.SetTrigger("FadeOut");
    }

    public void onFadeComplete()
    {
        SceneManager.LoadSceneAsync(sceneToLoad.name, LoadSceneMode.Single);
        Time.timeScale = 1.0f;
    }

    public void ReturnToMain()
    {
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
    }
}
