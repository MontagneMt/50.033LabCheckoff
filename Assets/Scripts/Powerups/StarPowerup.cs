using UnityEngine;

public class StarPowerup : BasePowerup
{
    // setup this object's type
    // instantiate variables
    protected override void Start()

    {
        base.Start(); // call base class Start()
        this.type = PowerupType.SuperStar;
        rigidBody.bodyType = RigidbodyType2D.Static;
        powerupCollider.enabled = false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && spawned)
        {

            PowerupManager.instance.powerupCollected.Invoke(this);
            // then destroy powerup (optional)
            DestroyPowerup();

        }
        else if (col.gameObject.layer == 10) // else if hitting Pipe, flip travel direction
        {
            if (spawned)
            {
                goRight = !goRight;
                rigidBody.AddForce(Vector2.right * 6 * (goRight ? 1 : -1), ForceMode2D.Impulse);
            }
        }
    }

    // interface implementation
    public override void SpawnPowerup()
    {
        powerupCollider.enabled = true;
        rigidBody.bodyType = RigidbodyType2D.Dynamic;
        spawned = true;
        rigidBody.AddForce(Vector2.right * 3, ForceMode2D.Impulse); // move to the right
    }

    // interface implementation
    public override void ApplyPowerup(MonoBehaviour i)
    {
        // make mario invincible
        PlayerMovement mario;
        bool result = i.TryGetComponent<PlayerMovement>(out mario);

        if (result) mario.MakeMarioInvincible();
    }
}