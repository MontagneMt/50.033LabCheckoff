using UnityEngine;

public class BrickPowerupController : MonoBehaviour, IPowerupController
{
    public Animator powerupAnimator;
    public BasePowerup powerup; 
    public bool isBreakable = false;
    public bool isVisible = true;

    void Start() {
        this.GetComponent<SpriteRenderer>().enabled = isVisible;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!powerup.hasSpawned)
            {
                this.GetComponent<SpriteRenderer>().enabled = true;
                this.GetComponent<Animator>().SetTrigger("bounce");
                powerupAnimator.SetTrigger("spawned");
                powerup.PlaySpawnAudio();
                if (!isBreakable)
                {
                    this.GetComponent<Animator>().SetTrigger("spawned");
                }
            } else
            {
                if (isBreakable)
                {
                    this.GetComponent<Animator>().SetTrigger("bounce");
                }
            }
        }
    }

    // used by animator
    public void Disable()
    {
    }
}