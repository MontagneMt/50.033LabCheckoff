using System.Collections;
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


    

    

    // Start is called before the first frame update
    void Start()
    {
        // Set to be 30 FPS
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
            marioAnimator.SetBool("onGround", onGroundState);
        }
    }

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
            }
        }
    }





    // FixedUpdate is called 50 times a second
    public float defaultGravity = 1;
    public float fallGravity = 3;
    public float maxFallSpeed = 10;
    void FixedUpdate()
    {
        if (alive && moving)
        {
            Move(faceRightState == true ? 1 : -1);
        }
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
    }

    void Move(int value)
    {

        Vector2 movement = new Vector2(value, 0);
        // check if it doesn't go beyond maxSpeed
        if (marioBody.velocity.magnitude < maxSpeed)
            marioBody.AddForce(movement * speed);
    }

    public void MoveCheck(int value)
    {
        if (value == 0)
        {
            moving = false;
        }
        else
        {
            FlipMarioSprite(value);
            moving = true;
            Move(value);
        }
    }
    private bool jumpedState = false;

    public void Jump()
    {
        if (alive && onGroundState)
        {
            // jump
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            jumpedState = true;
            // update animator state
            marioAnimator.SetBool("onGround", onGroundState);

        }
    }

    
    public void JumpHold()
    {
        if (alive && jumpedState)
        {
            // jump higher
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
    
