using UnityEngine;

public class QuestionBoxPowerupController : MonoBehaviour, IPowerupController
{
    public Animator powerupAnimator;
    public BasePowerup powerup; // reference to this question box's powerup
    Vector3 initialPosition;

    void Start() 
    {
        initialPosition = transform.localPosition;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && !powerup.hasSpawned)
        {
            // show disabled sprite
            this.GetComponent<Animator>().SetTrigger("spawned");
            powerup.PlaySpawnAudio();
            // spawn the powerup
            powerupAnimator.SetTrigger("spawned");
        }
    }

    // used by animator
    public void Disable()
    {
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        transform.localPosition = initialPosition;
    }
}