using System.Collections;
<<<<<<< HEAD
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour, IPowerupApplicable
{
    public GameConstants gameConstants;
    // public AudioClips audioClips;
    private bool onGroundState = true;
    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    // for animation
    public Animator marioAnimator;
    public AudioSource marioAudio;
    public AudioSource marioDeathAudio;
    // public MarioActions marioActions;

    [System.NonSerialized]
    public bool alive = true;
    private int collisionLayerMask = (1 << 3) | (1 << 6) | (1 << 7) | (1 << 10);
    private bool moving = false;
    private bool jumpedState = false;
    public PlayerManager playerManager;
    public bool isInvincible = false;

    public void Awake()
    {
        GameManager.instance.gameRestart.AddListener(GameRestart);
        GameManager.instance.powerupAffectsPlayer.AddListener(RequestPowerupEffect);
    }
=======
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    private Rigidbody2D marioBody;
    public float maxSpeed = 20;
    public float upSpeed = 10;
    private bool onGroundState = true;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    public TextMeshProUGUI scoreText;
    public GameObject enemies;
    public JumpOverGoomba jumpOverGoomba;
    public GameObject gameOverCanvas;
    public Animator marioAnimator;
    public AudioSource marioAudio;
    public AudioClip marioDeath;
    public float deathImpulse = 15;
    public Transform gameCamera;
    int collisionLayerMask = (1 << 3) | (1 << 6) | (1 << 7);
    private bool moving = false;
    



    // state
    [System.NonSerialized]
    public bool alive = true;


    

    
>>>>>>> 61e7f24223104f05955f697e592929f862e9089a

    // Start is called before the first frame update
    void Start()
    {
        // Set to be 30 FPS
<<<<<<< HEAD
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();

        // update animator state
        marioAnimator.SetBool("onGround", onGroundState);

    }

    void Update()
    {
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
    }

    public void RequestPowerupEffect(IPowerup i)
    {
        i.ApplyPowerup(this);
    }

    public void MakeMarioInvincible()
    {
        // StartCoroutine(Invincible());
        Debug.Log("Starman invincibility to be implemented");
    }

    // IEnumerator Invincible()
    // {
    //     isInvincible = true;
    //     AudioManager.instance.PlayAudio(audioClips.starInvincibilityAudio);
    //     for (float seconds = 0; seconds < 10; seconds += 1)
    //     {
    //         yield return new WaitForSecondsRealtime(1f);
    //     }
    //     AudioManager.instance.StopAudio();
    //     isInvincible = false;
    // }

    void FlipMarioSprite(int value)
    {
        // check if game is paused
        if (!GameManager.instance.isPaused)
        {
            if (value == -1 && faceRightState)
            {
                faceRightState = false;
                marioSprite.flipX = true;
                if (marioBody.velocity.x > 0.05f)
                    marioAnimator.SetTrigger("onSkid");
            }
            else if (value == 1 && !faceRightState)
            {
                faceRightState = true;
                marioSprite.flipX = false;
                if (marioBody.velocity.x < -0.05f)
                    marioAnimator.SetTrigger("onSkid");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (((collisionLayerMask & (1 << col.transform.gameObject.layer)) > 0) && !onGroundState)
        {
            onGroundState = true;
            // updating animator state
=======
        Application.targetFrameRate =  30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        gameOverCanvas.SetActive(false);
        marioAnimator.SetBool("onGround", onGroundState);
        


    }

    // Update is called once per frame
    void Update()
    {
      marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
      
    }
    void OnCollisionEnter2D(Collision2D col)
    {
      if (((collisionLayerMask & (1 << col.transform.gameObject.layer)) > 0) & !onGroundState)
        {
            onGroundState = true;
            // update animator state
>>>>>>> 61e7f24223104f05955f697e592929f862e9089a
            marioAnimator.SetBool("onGround", onGroundState);
        }
    }

<<<<<<< HEAD
    // Game over condition
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemies") && alive && !isInvincible)
        {
            float eps = 0.3f;
            if ((other.transform.position.y + other.bounds.size.y - eps) < transform.position.y)
            {
                // crush
                FindObjectOfType<EnemyManager>().CrushGoomba(other.gameObject);
                GameManager.instance.IncreaseScore(1);
                marioBody.AddForce(Vector2.up * 25, ForceMode2D.Impulse);
            }
            else 
            {
                playerManager.MarioKilled();
                // play death animation
                alive = false;
                marioAnimator.Play("Mario-die");
                marioDeathAudio.PlayOneShot(marioDeathAudio.clip);
=======
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && alive)
        {
            if (transform.position.y > other.transform.position.y + 0.5f)
            {
                GoombaEffects goombaEffects = other.gameObject.GetComponent<GoombaEffects>();
                if (goombaEffects != null)
                {
                    goombaEffects.FlattenGoomba(); // 调用新脚本的方法
                }
                else
                {
                    Debug.LogError("Goomba lacks the GoombaEffects component!");
                }

                jumpOverGoomba.score++;
                scoreText.text = "Score: " + jumpOverGoomba.score.ToString();
                // 我们不再立即摧毁Goomba，因为我们想显示扁平化的效果和播放音效
            }
            else
            {
                Debug.Log("Collided with goomba from the side!");
                marioAnimator.Play("Mario_Die");
                marioAudio.PlayOneShot(marioDeath);
                alive = false;
>>>>>>> 61e7f24223104f05955f697e592929f862e9089a
            }
        }
    }

<<<<<<< HEAD
    // FixedUpdate is called 50 times a second
=======




    // FixedUpdate is called 50 times a second
    public float defaultGravity = 1;
    public float fallGravity = 3;
    public float maxFallSpeed = 10;
>>>>>>> 61e7f24223104f05955f697e592929f862e9089a
    void FixedUpdate()
    {
        if (alive && moving)
        {
            Move(faceRightState == true ? 1 : -1);
        }
<<<<<<< HEAD
=======
        if (marioBody.velocity.y < 0)
        {

            marioBody.gravityScale = fallGravity;
            // Fall speed cap
            marioBody.velocity = new Vector2(marioBody.velocity.x, Mathf.Max(marioBody.velocity.y, -maxFallSpeed));
        }
        else
        {
            marioBody.gravityScale = defaultGravity;
        }
    }


    public void RestartButtonCallback(int input)
    {
        Debug.Log("Restart!");
        // reset everything
        ResetGame();
        // resume time
        Time.timeScale = 1.0f;
    }

    public void ResetGame()
    {
        // reset position
        marioBody.transform.position = new Vector3(-5.33f, -4.69f, 0.0f);
        // reset sprite direction
        faceRightState = true;
        marioSprite.flipX = false;
        // reset score
        scoreText.text = "Score: 0";
        // reset goomba
        foreach (Transform eachChild in enemies.transform)
        {
            eachChild.gameObject.SetActive(true);
            eachChild.GetComponent<GoombaEffects>()?.ResetGoomba();
            eachChild.transform.localPosition = eachChild.GetComponent<EnemyMovement>().startPosition;
        }





        // reset score
        jumpOverGoomba.score = 0;
        gameOverCanvas.SetActive(false);

        
        // reset animation
        marioAnimator.SetTrigger("gameRestart");
        alive = true;
        // reset camera position
        gameCamera.position = new Vector3(0, -2, -10);

    }
    void PlayJumpSound()
    {
        // play jump sound
        marioAudio.PlayOneShot(marioAudio.clip);
    }
    void PlayDeathImpulse()
    {
        marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
    }
    void GameoverScene()
    {
        Time.timeScale = 0.0f;
        gameOverCanvas.SetActive(true);
    }

    void FlipMarioSprite(int value)
    {
        if (value == -1 && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
            if (marioBody.velocity.x > 0.05f)
                marioAnimator.SetTrigger("onSkid");

        }

        else if (value == 1 && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
            if (marioBody.velocity.x < -0.05f)
                marioAnimator.SetTrigger("onSkid");
        }
>>>>>>> 61e7f24223104f05955f697e592929f862e9089a
    }

    void Move(int value)
    {
<<<<<<< HEAD
        Vector2 movement = new Vector2(value, 0);
        // check maxSpeed
        if (marioBody.velocity.magnitude < gameConstants.maxSpeed)
            marioBody.AddForce(movement * gameConstants.speed);
=======

        Vector2 movement = new Vector2(value, 0);
        // check if it doesn't go beyond maxSpeed
        if (marioBody.velocity.magnitude < maxSpeed)
            marioBody.AddForce(movement * speed);
>>>>>>> 61e7f24223104f05955f697e592929f862e9089a
    }

    public void MoveCheck(int value)
    {
<<<<<<< HEAD
        if (value == 0) moving = false;
=======
        if (value == 0)
        {
            moving = false;
        }
>>>>>>> 61e7f24223104f05955f697e592929f862e9089a
        else
        {
            FlipMarioSprite(value);
            moving = true;
            Move(value);
        }
    }
<<<<<<< HEAD
=======
    private bool jumpedState = false;
>>>>>>> 61e7f24223104f05955f697e592929f862e9089a

    public void Jump()
    {
        if (alive && onGroundState)
        {
            // jump
<<<<<<< HEAD
            marioBody.AddForce(Vector2.up * gameConstants.upSpeed, ForceMode2D.Impulse);
=======
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
>>>>>>> 61e7f24223104f05955f697e592929f862e9089a
            onGroundState = false;
            jumpedState = true;
            // update animator state
            marioAnimator.SetBool("onGround", onGroundState);
<<<<<<< HEAD
        }
    }

=======

        }
    }

    
>>>>>>> 61e7f24223104f05955f697e592929f862e9089a
    public void JumpHold()
    {
        if (alive && jumpedState)
        {
            // jump higher
<<<<<<< HEAD
            marioBody.AddForce(Vector2.up * gameConstants.upSpeed, ForceMode2D.Impulse);
            jumpedState = false;
        }
    }

    public void GameRestart()
    {
        // // // reset mario position
        // marioBody.transform.position = new Vector3(-6.59f, -2.78f, 0.0f);
        // // // reset sprite direction
        // faceRightState = true;
        // marioSprite.flipX = false;

        // marioAnimator.SetTrigger("gameRestart");
        alive = true;

        // Reload entire scene
        Scene scene = SceneManager.GetActiveScene();
        FindAnyObjectByType<SceneLoader>().LoadScene(scene);
    }

    private void GameOver()
    {
        GameManager.instance.GameOver();
    }
    void PlayJumpSound()
    {
        marioAudio.PlayOneShot(marioAudio.clip);
    }
    void PlayDeathImpulse()
    {
        marioBody.AddForce(Vector2.up * gameConstants.deathImpulse, ForceMode2D.Impulse);
    }
}
=======
            marioBody.AddForce(Vector2.up * upSpeed * 30, ForceMode2D.Force);
            jumpedState = false;

        }
    }
    public void GameRestart()
    {
        // reset position
        marioBody.transform.position = new Vector3(-5.33f, -4.69f, 0.0f);
        // reset sprite direction
        faceRightState = true;
        marioSprite.flipX = false;

        // reset animation
        marioAnimator.SetTrigger("gameRestart");
        alive = true;

        // reset camera position
        gameCamera.position = new Vector3(0, 0, -10);
    }
    

}
    
>>>>>>> 61e7f24223104f05955f697e592929f862e9089a
