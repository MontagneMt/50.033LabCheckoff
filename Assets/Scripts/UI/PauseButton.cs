using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseButtonController : MonoBehaviour, IInteractiveButton
{
    public Sprite pauseIcon;
    public Sprite playIcon;
    public GameObject pauseCanvas;
    private Image image;
    private AudioSource pauseAudio;
    // Example usage:
    public AudioMixer mixer;
    private AudioMixerSnapshot pausedSnapshot;

    
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        pauseAudio = GetComponent<AudioSource>();
        pauseCanvas.SetActive(false);
        pausedSnapshot = mixer.FindSnapshot("paused");
    }

    public void ButtonClick()
    {
        GameManager.instance.isPaused ^= true;
        bool isPaused = GameManager.instance.isPaused;
        Time.timeScale = isPaused ? 0.0f : 1.0f;
        if (isPaused)
        {
            image.sprite = playIcon;
            pauseAudio.PlayOneShot(pauseAudio.clip);
            pauseCanvas.SetActive(true);
            pausedSnapshot.TransitionTo(0f);
        }
        else
        {
            image.sprite = pauseIcon;
            pauseCanvas.SetActive(false);
            mixer.FindSnapshot("default").TransitionTo(0.5f);
        }
    }
}

