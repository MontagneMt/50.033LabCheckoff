using UnityEngine;


public abstract class BasePowerup : MonoBehaviour, IPowerup
{
    protected PowerupType type;
    protected bool spawned = false;
    protected bool consumed = false;
    protected bool goRight = true;
    protected Rigidbody2D rigidBody;
    protected Collider2D powerupCollider;
    protected AudioSource powerupAudio;
    public AudioClip powerupCollectedClip;

    // base methods
    protected virtual void Start(){
        rigidBody = GetComponent<Rigidbody2D>();
        powerupCollider = GetComponent<Collider2D>();
        powerupAudio = GetComponent<AudioSource>();
    }

    // interface methods
    // 1. concrete methods
    public PowerupType powerupType
    {
        get // getter
        {
            return type;
        }
    }

    public bool hasSpawned
    {
        get // getter
        {
            return spawned;
        }
    }

    public void DestroyPowerup()
    {
        Destroy(this.gameObject);
    }
    public void PlaySpawnAudio()
    {
        powerupAudio.PlayOneShot(powerupAudio.clip);
    }
    public void PlayCollectedAudio()
    {
        AudioManager.instance.PlayAudio(powerupCollectedClip);
    }


    // 2. abstract methods, must be implemented by derived classes
    public abstract void SpawnPowerup();
    public abstract void ApplyPowerup(MonoBehaviour i);
}
